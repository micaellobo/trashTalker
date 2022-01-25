using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Helpers;
using TrashTalker.Models;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Services
{
    public class RouteOptimizationServiceService : IRouteOptimizationService
    {
        private readonly IDistanceMatrixService _distanceMatrixService;
        private readonly IMeasurementRepository _measurementRepository;

        public RouteOptimizationServiceService(IDistanceMatrixService distanceMatrixService,
            IMeasurementRepository measurementRepository)
        {
            _distanceMatrixService = distanceMatrixService;
            _measurementRepository = measurementRepository;
        }

        /// <inheritdoc/>
        public async Task<(List<RecycleBin> recBins, int distance, int duration)> generateRoutePath(List<Route> routesDb,
            List<RecycleBin> recycleBins, double minPercentageOccupied, TimeSpan maxDuration, DateTime dateRoute)
        {
            var distance = 0;
            var duration = 0;
            var indexNextRecBin = 0;
            var indexLast = 0;

            var distanceMatrix = await _distanceMatrixService.getDistanceMatrix(recycleBins);

            var recBinsPath = new List<RecycleBin>();
            var visited = new bool[distanceMatrix.rows.Count];

            //mark start as visited
            visited[0] = true;
            for (var i = 0; i < distanceMatrix.rows.Count - 1; i++)
            {
                //Finds the next closest RecBin 
                var (recBinElement, indexClosestRecBin) = findClosesRecBin(distanceMatrix.rows[indexNextRecBin], visited);
                var recBinDb = recycleBins[indexClosestRecBin - 1];

                //Get the info about the return to start point
                var infoReturn = distanceMatrix.rows[indexClosestRecBin].elements.ElementAt(0);

                var readyToCollect = hasAnyContainersWithPercentageReadyToCollect(recBinDb, routesDb, minPercentageOccupied, 100, dateRoute);

                var isBetweenDurationLimit =
                    isBetweenDurationLimitToReturn(duration, recBinElement.duration.value, infoReturn, maxDuration);

                if (readyToCollect && isBetweenDurationLimit)
                {
                    distance += recBinElement.distance.value;
                    duration += recBinElement.duration.value;
                    indexLast = indexClosestRecBin;
                    recBinsPath.Add(recBinDb);
                }

                visited[indexClosestRecBin] = true;
                indexNextRecBin = indexClosestRecBin;
            }


            //Some the return to the start point
            var returnInfo = distanceMatrix.rows[indexLast].elements.ElementAt(0);
            distance += returnInfo.distance.value;
            duration += returnInfo.duration.value;

            // foreach (var b in visited)
            //     Console.Write(b + " ");
            Console.WriteLine();

            //Marcar os Ecopontos nao selecionados como nao visitados
            for (var i = 0; i < recycleBins.Count; i++)
                if (!recBinsPath.Contains(recycleBins[i]))
                    visited[i + 1] = false;

            // foreach (var b in visited)
            //     Console.Write(b + " ");
            Console.WriteLine(Environment.NewLine + "\nOrdem de pedidos matrix");

            Console.Write("ESTG  ");
            foreach (var recycleBin in recycleBins)
                Console.Write(recycleBin.city + "  ");

            Console.Write(Environment.NewLine + Environment.NewLine);
            foreach (var row in distanceMatrix.rows)
            {
                foreach (var element in row.elements)
                    Console.Write($"{(element.distance.value, element.duration.value)}  ");
                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("\nAntes optomizacao");
            foreach (var recycleBin in recBinsPath)
                Console.Write(recycleBin.city + "  ");
            Console.WriteLine();
            Console.WriteLine((distance / 1000.0, TimeSpan.FromSeconds(duration)));

            // Caso ainda tenha tempo capacidade e e ainda Ecopontos a que nao foi, vai adicionar os que estao mais proximos
            var (newPathRecBins, newDistance, newDuration) = addCloseRecBins(distanceMatrix, routesDb, visited, recycleBins, recBinsPath,
                minPercentageOccupied, duration, distance, maxDuration, dateRoute);

            recBinsPath = newPathRecBins;
            distance = newDistance;
            duration = newDuration;

            return (recBinsPath, distance, duration);
        }

        /// <summary>
        /// After the selections of the <see cref="RecycleBin"/> with higher occupation , this method selects if possible closer <see cref="RecycleBin"/>
        /// </summary>
        /// <param name="distanceMatrix"><see cref="DistanceMatrix"/> information about distances between all places</param>
        /// <param name="routesDb"> current existing <see cref="Route"/> </param>
        /// <param name="visited">List of visited </param>
        /// <param name="recycleBinsDb">List of <see cref="RecycleBin"/> in the database</param>
        /// <param name="currentPath">current path of the Route</param>
        /// <param name="minPercentageOccupied">min percentage</param>
        /// <param name="currentDuration">current duration of the <see cref="Route"/></param>
        /// <param name="currentDistance"> current distance of the <see cref="Route"/></param>
        /// <param name="maxDuration"> max duratio of the <see cref="Route"/></param>
        /// <param name="dateRoute"> <see cref="DateTime"/> of the <see cref="Route"/> to generate</param>
        /// <returns> New List of <see cref="RecycleBin"/> new duration and new distance </returns>
        private (List<RecycleBin> newPathRecBins, int newDistance, int newDuration) addCloseRecBins(DistanceMatrix distanceMatrix,
            List<Route> routesDb, IList<bool> visited, List<RecycleBin> recycleBinsDb, List<RecycleBin> currentPath, double minPercentageOccupied,
            int currentDuration, int currentDistance, TimeSpan maxDuration, DateTime dateRoute)
        {
            var tempDuration = currentDuration;
            var tempDistance = currentDistance;


            // Caso ainda tenha tempo capacidade e e ainda Ecopontos a que nao foi, vai adicionar os que estao mais proximos
            while (currentDuration <= maxDuration.TotalSeconds && visited.Any(visit => visit == false))
            {
                var (infoPredecessorToBestNoVisited, indexBestNotVisited, indexPredecessor) =
                    findBestPredecessorFromRecBinNotVisited(distanceMatrix, visited);
                var recBinDbNotVisited = recycleBinsDb[indexBestNotVisited - 1];
                var hasPercentageToCollect = recBinDbNotVisited.containers.Any(container =>
                    isContainerInRangePercentageToCollect(container, routesDb, dateRoute, 60, minPercentageOccupied));

                if (hasPercentageToCollect)
                {
                    //encontrar a distancia que vai de por exemplo de A - B e remover essa distancia e adicionar a distancia de A a C mais de C a B
                    // que passa a ficar a distancia de A - C - B

                    //Caso o novo Ecoponto seja colocado no inicio
                    if (indexPredecessor == 0)
                    {
                        Console.WriteLine("Adiciona no inicio");
                        var indexOldFirstRecBin = currentPath.IndexOf(recycleBinsDb[indexPredecessor - 1]) + 1;

                        var infoHomeToNext = distanceMatrix.rows[0].elements[indexOldFirstRecBin];
                        tempDuration -= infoHomeToNext.duration.value;
                        tempDistance -= infoHomeToNext.distance.value;

                        var infoHomeToNotVisited = distanceMatrix.rows[0].elements[indexBestNotVisited];
                        tempDistance += infoHomeToNotVisited.distance.value;
                        tempDuration += infoHomeToNotVisited.duration.value;

                        var infoNewReturn = distanceMatrix.rows[indexBestNotVisited].elements[indexOldFirstRecBin];
                        tempDistance += infoNewReturn.distance.value;
                        tempDuration += infoNewReturn.duration.value;

                        //Se depois destas alteracoes ainda ficar dentro da duracao maxima
                        if (tempDuration <= maxDuration.TotalSeconds)
                        {
                            currentDistance = tempDistance;
                            currentDuration = tempDuration;
                            currentPath.Insert(0, recBinDbNotVisited);
                        }
                    }
                    else
                    {
                        var indexOldNextPredecessor = currentPath.IndexOf(recycleBinsDb[indexPredecessor - 1]) + 1;
                        //Caso o novo Ecoponto na Rota seja o ultimo a ser recolhido
                        if (indexOldNextPredecessor == 5)
                        {
                            Console.WriteLine("Adiciona na fim");
                            //Remove from old last to home
                            var infoOldReturn = distanceMatrix.rows[indexPredecessor].elements[0];
                            tempDuration -= infoOldReturn.duration.value;
                            tempDistance -= infoOldReturn.distance.value;

                            //add next Recbin to home
                            var infoNewReturn = distanceMatrix.rows[indexBestNotVisited].elements[0];
                            tempDistance += infoNewReturn.distance.value;
                            tempDuration += infoNewReturn.duration.value;

                            //add old last to new RecBin
                            var infoBestNotVisitedw = distanceMatrix.rows[indexPredecessor].elements[indexBestNotVisited];
                            tempDistance += infoBestNotVisitedw.distance.value;
                            tempDuration += infoBestNotVisitedw.duration.value;
                        }
                        else
                        {
                            Console.WriteLine("Adiciona no meio");
                            //remove a distancia e duracao de A - B
                            var infoToOldNextPredecessor = distanceMatrix.rows[indexPredecessor].elements[indexOldNextPredecessor];
                            tempDuration -= infoToOldNextPredecessor.duration.value;
                            tempDistance -= infoToOldNextPredecessor.distance.value;

                            //adicionar distancia e duracao de A - C
                            tempDuration += infoPredecessorToBestNoVisited.duration.value;
                            tempDistance += infoPredecessorToBestNoVisited.distance.value;

                            //adicionar distancia e duracao de C - B
                            var infoNotVisitedToOldNextPredecessor = distanceMatrix.rows[indexBestNotVisited].elements[indexOldNextPredecessor];
                            tempDistance += infoNotVisitedToOldNextPredecessor.distance.value;
                            tempDuration += infoNotVisitedToOldNextPredecessor.duration.value;
                        }

                        //Se depois destas alteracoes ainda ficar dentro da duracao maxima
                        if (tempDuration <= maxDuration.TotalSeconds)
                        {
                            currentDistance = tempDistance;
                            currentDuration = tempDuration;
                            currentPath.Insert(indexOldNextPredecessor, recBinDbNotVisited);
                        }
                    }
                }

                visited[indexBestNotVisited] = true;
            }

            Console.WriteLine("\nDepois optomizacao");
            foreach (var recycleBin in currentPath)
                Console.Write(recycleBin.city + "  ");
            Console.WriteLine();
            Console.WriteLine((currentDistance / 1000.0, TimeSpan.FromSeconds(currentDuration)));
            return (currentPath, currentDistance, currentDuration);
        }

        /// <summary>
        /// Finds the closest <see cref="RecycleBin"/> not visited
        /// </summary>
        /// <param name="distanceMatrix"><see cref="DistanceMatrix"/> information about distances between all places</param>
        /// <param name="visited">List of visited <see cref="RecycleBin"/> </param>
        /// <returns>Information about the closed <see cref="RecycleBin"/></returns>
        private (Element recBinElement, int indexNotVisited, int indexPredecessor) findBestPredecessorFromRecBinNotVisited(
            DistanceMatrix distanceMatrix,
            IList<bool> visited)
        {
            //Encontra quais é que ainda nao foram visitados
            var indexesNotVisited = visited.Select((isVisited, index) => new {isVisited, index})
                .Where(o => !o.isVisited)
                .Select(o => o.index).ToList();

            //Encontra os que ja foram visitados
            var indexesVisited = visited.Select((isVisited, index) => new {isVisited, index})
                .Where(o => o.isVisited)
                .Select(o => o.index).ToList();

            // pos 0 e o ponto de partida aquando o pedido a API Google
            // indicar que o melhor é o seguinte e procurar se existe outro melhor
            var indexPredecessor = indexesVisited.First();
            var indexBest = indexesNotVisited.First();
            var best = distanceMatrix.rows[indexPredecessor].elements[indexBest];

            foreach (var indexVisited in indexesVisited)
            {
                foreach (var indexNotVisited in indexesNotVisited)
                {
                    if (distanceMatrix.rows[indexVisited].elements[indexNotVisited].distance.value >= best.distance.value) continue;
                    indexPredecessor = indexVisited;
                    indexBest = indexNotVisited;
                    best = distanceMatrix.rows[indexPredecessor].elements[indexBest];
                }
            }

            return (best, indexBest, indexPredecessor);
        }

        /// <summary>
        /// Determines if specific container is in a range of percentage occupation  
        /// </summary>
        /// <param name="container"><see cref="Container"/> to determine</param>
        /// <param name="routesDb"> current existing <see cref="Route"/> </param>
        /// <param name="dateRoute"><see cref="DateTime"/> of the <see cref="Route"/></param>
        /// <param name="lowerBoundPercentageOccupied">lower bound</param>
        /// <param name="upperBoundPercentageOccupied">upper bound</param>
        /// <returns>True if the <see cref="Container"/> is in the range of percentage</returns>
        private bool isContainerInRangePercentageToCollect(Container container, List<Route> routesDb, DateTime dateRoute,
            double lowerBoundPercentageOccupied, double upperBoundPercentageOccupied)
        {
            //Encontra a Rota mais recente onde esta presente este container
            var mostRecentRouteWithContainer = routesDb
                .Where(route => route.status == StatusRoute.ONGOING || route.status == StatusRoute.PLANNED && route.dateBegin <= dateRoute &&
                    route.collectPoints.Any(point => point.recycleBin.containers.Any(container1 => container1.id.Equals(container.id))))
                .OrderByDescending(route => route.dateBegin).FirstOrDefault();

            //Caso este container esteja numa outra Rota, o calculo sera tendo em conta a sua existencia na outra Rota
            var (containerPercentageToCalculate, dateRouteToCalculate) = mostRecentRouteWithContainer == null
                ? Tuple.Create(container.currentPercOccupied, DateTime.Now)
                : Tuple.Create(0f, mostRecentRouteWithContainer.dateBegin);

            var previsionOccupation = containerPercentageToCalculate +
                                      container.avgGrowthOccupiedVolumePerDay * (dateRoute - dateRouteToCalculate).TotalDays;
            
            previsionOccupation = previsionOccupation > 100 ? 100 : previsionOccupation;
            
            return previsionOccupation >= lowerBoundPercentageOccupied * 100 && previsionOccupation <= upperBoundPercentageOccupied;
        }

        /// <summary>
        /// Detertmine if an specific <see cref="RecycleBin"/> is in the range of percentage occupation 
        /// </summary>
        /// <param name="recycleBin"></param>
        /// <param name="routesDb"></param>
        /// <param name="minPercentage"></param>
        /// <param name="dateRoute"></param>
        /// <returns></returns>
        private bool hasAnyContainersWithPercentageReadyToCollect(RecycleBin recycleBin, List<Route> routesDb, double minPercentage,
            double upperBound, DateTime dateRoute)
        {
            return recycleBin.containers.Any(container =>
                isContainerInRangePercentageToCollect(container, routesDb, dateRoute, minPercentage, upperBound));
        }

        /// <summary>
        /// Determines if going to the next <see cref="RecycleBin"/> if between the duration limit
        /// </summary>
        /// <param name="durationActual">actual duration</param>
        /// <param name="durationToNext">duration to the next <see cref="RecycleBin"/></param>
        /// <param name="infoReturn"><see cref="Element"/> information about the return path</param>
        /// <param name="maxDuration">max duration</param>
        /// <returns>True if going to the next <see cref="RecycleBin"/> is between the duration limit</returns>
        private bool isBetweenDurationLimitToReturn(int durationActual, int durationToNext, Element infoReturn, TimeSpan maxDuration)
        {
            var nextDuration = durationActual + durationToNext + infoReturn.duration.value;
            return nextDuration <= maxDuration.TotalSeconds;
        }


        /// <summary>
        /// Finds the closest <see cref="RecycleBin"/> 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="visited">List of visited <see cref="RecycleBin"/> </param>
        /// <returns>Information about the closed <see cref="RecycleBin"/></returns>
        private (Element recBinElement, int index) findClosesRecBin(Row row, IList<bool> visited)
        {
            // pos 0 e o ponto de partida aquando o pedido a API Google
            // indicar que o melhor é o seguinte e procurar se existe outro melhor
            var indexBest = 1;
            var best = row.elements[indexBest];
            for (var i = 2; i < row.elements.Count; i++)
            {
                if (indexBest == i) continue;
                if (visited[i]) continue;
                if (!visited[indexBest] && (best.distance.value < row.elements[i].distance.value)) continue;
                best = row.elements[i];
                indexBest = i;
            }

            return (best, indexBest);
        }
    }

    /// <summary>
    /// Interface that represents the optimization if an <see cref="Route"/>
    /// </summary>
    public interface IRouteOptimizationService
    {
        /// <summary>
        /// Generates the path for an <see cref="Route"/>
        /// </summary>
        /// <param name="recycleBins"><see cref="List{T}"/> of possible <see cref="RecycleBin"/> that can be in the <see cref="Route"/> </param>
        /// <param name="minPercentageOccupied">minimum percentage for an <see cref="Container"/> be in the <see cref="Route"/></param>
        /// <param name="maxDuration"><see cref="TimeSpan"/> max duration for the <see cref="Route"/></param>
        /// <returns><see cref="List{T}"/> of <see cref="RecycleBin"/>, distance, duration</returns>
        Task<(List<RecycleBin> recBins, int distance, int duration)> generateRoutePath(List<Route> routesDb, List<RecycleBin> recycleBins,
            double minPercentageOccupied,
            TimeSpan maxDuration, DateTime dateRoute);
    }
}