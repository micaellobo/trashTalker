using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrashTalker.Dto.Route;
using TrashTalker.Helpers;
using TrashTalker.Models;
using static TrashTalker.Config;
using TrashTalker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrashTalker.Models.Enumerations;

namespace TrashTalker.Database.Repositories.RouteRepository
{
    /// <summary>
    /// This Class represents a Route Repository that contains all the necessary methods for Route management.
    /// This Repository is also important because it establishes a direct connection to the SQL database.
    /// </summary>
    public class RouteRepository : IRouteRepository
    {
        /// <summary>
        /// A DbContext instance that represents a session with the database that can be used to query and save
        /// instances of your entities.
        /// </summary>
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Distance matrix service
        /// </summary>
        private readonly IDistanceMatrixService _distanceMatrixService;

        private readonly IRouteOptimizationService _routeOptimizationService;

        /// <summary>
        /// Constructor method for the Route repository.
        /// </summary>
        /// <param name="databaseContext"> of the database</param>
        /// <param name="distanceMatrixService">distance matrix service</param>
        public RouteRepository(DatabaseContext databaseContext, IDistanceMatrixService distanceMatrixService,
            IRouteOptimizationService routeOptimizationService)
        {
            _dbContext = databaseContext;
            _distanceMatrixService = distanceMatrixService;
            _routeOptimizationService = routeOptimizationService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Route>> getRoutes()
        {
            return await _dbContext.Routes.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Route>> getRoutesEmployeeLoggedIn(Guid employeeId)
        {
            return await _dbContext.Routes.FromSqlInterpolated(
                    $"SELECT * FROM Routes WHERE  employeeid = {employeeId.ToString()}")
                .ToListAsync();
        }


        public async Task<IList<Route>> getRoutesByStateEmployeeLoggedIn(StatusRoute status, Guid employeeId)
        {
            return await _dbContext.Routes
                .Where(route => route.employee.id == employeeId && route.status == status).ToListAsync();
        }

        /// <inheritdoc/>
        public Task<Route> getRouteByIdEmployeeLoggedIn(Guid routeId, Guid employeeId)
        {
            var route = _dbContext.Routes.FromSqlInterpolated(
                    $"SELECT * FROM Routes WHERE id = {routeId.ToString()} AND employeeid = {employeeId.ToString()}")
                .ToArray();
            return Task.FromResult(route.Length == 0 ? null : route[0]);
        }

        /// <inheritdoc/>
        public async Task<Route> getRoute(Guid id)
        {
            return await _dbContext.Routes.FirstOrDefaultAsync(route => route.id == id);
        }

        /// <inheritdoc/>
        public async Task<Route> addRoute(CreateRouteDto createRouteDto)
        {
            var route = createRouteDto.asRoute();
            var collectPoints = new List<CollectPoint>();
            foreach (var (recBinId, index) in createRouteDto.recycleBinIds.Select((recBinId, index) => (recBinId, index)))
            {
                var recycleBin = await _dbContext.RecycleBins.FirstOrDefaultAsync(bin => bin.id == recBinId);
                if (recycleBin == null)
                    throw new KeyNotFoundException($"The RecycleBin with the id \"{recBinId}\" does not exist");
                if (recycleBin.status == Status.INACTIVE)
                    throw new KeyNotFoundException($"The RecycleBin with the id \"{recBinId}\" is Inactive");
                collectPoints.Add(new CollectPoint()
                {
                    order = index,
                    recycleBin = recycleBin,
                    route = route
                });
            }

            route.collectPoints = new List<CollectPoint>(collectPoints.OrderBy(point => point.order));

            route.employee = await _dbContext.Users.FirstOrDefaultAsync(user => user.id == createRouteDto.employeeId);

            //Calcular a distancia e duracao estimadas
            var (distance, duration) = await _distanceMatrixService.getEstimatedDurationAndDistance(route);

            route.estimatedDuration = TimeSpan.FromSeconds(Convert.ToDouble(duration));
            route.distanceEstimatedKm = distance;

            if (TimeSpan.FromSeconds(Convert.ToDouble(duration)) > MAX_DURATION_ROUTE)
                throw new InvalidOperationException($"The Route cannot exceed {MAX_DURATION_ROUTE} of duration");

            var routeToAdd = await _dbContext.Routes.AddAsync(route);

            await _dbContext.SaveChangesAsync();

            return routeToAdd.Entity;
        }

        /// <inheritdoc/>
        public async Task<Route> createPendingRoute(CreateAutomaticRoute createAutomatic)
        {
            var routesDb = await _dbContext.Routes.ToListAsync();

            var countRoutes = routesDb.Count;

            var employee = await _dbContext.Users.FirstOrDefaultAsync(emp => emp.id == createAutomatic.employeeId);

            var route = createAutomatic.asRoutePending($"Automatic Route {countRoutes}");

            var listAsync = await _dbContext.RecycleBins.Where(bin => bin.status == Status.ACTIVE).ToListAsync();

            if (listAsync.Count <= 2)
                throw new Exception("Nao existem ecopontos suficientes na base dados");

            if (listAsync.Count > 9)
                throw new Exception("Limite da GOOGLE API atingido, apenas é possivel ter 9 Ecopontos ativos");

            var (recBins, distance, duration) =
                await _routeOptimizationService.generateRoutePath(routesDb, listAsync, MAX_ALERT_CAPACITY, MAX_DURATION_ROUTE, route.dateBegin);

            route.collectPoints = new List<CollectPoint>(recBins.Select((x, index) =>
                new CollectPoint
                {
                    order = index,
                    route = route,
                    recycleBin = x
                }));
            route.estimatedDuration = TimeSpan.FromSeconds(Convert.ToDouble(duration));
            route.distanceEstimatedKm = distance;
            route.employee = employee;

            //Verifica se apos gerar a Rota existem pelo menos 2 Ecopontos selecionados
            if (route.collectPoints.Count < 2)
                throw new InvalidOperationException($"De momento nao existem ecopontos prontos para recolha");

            var routeToAdd = await _dbContext.Routes.AddAsync(route);

            await _dbContext.SaveChangesAsync();

            return routeToAdd.Entity;
        }

        public async Task deletePendingRoute(Guid routeId)
        {
            var collectPoints = await _dbContext.CollectPoint.Where(collectPoint => collectPoint.route.id.Equals(routeId)).ToListAsync();

            var route = await this.getRoute(routeId);

            if (route is null)
                throw new InvalidOperationException($"The route with id \"{routeId}\" does not exist");

            _dbContext.CollectPoint.RemoveRange(collectPoints);
            _dbContext.Routes.Remove(route);

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<Route> updateRoute(Route route)
        {
            var routeDb = await _dbContext.Routes.FirstOrDefaultAsync(routeDb => routeDb.id == route.id);
            await _dbContext.SaveChangesAsync();
            return routeDb;
        }

        public async Task<Route> confirmeRouteCreation(Route route)
        {
            var routeDb = await _dbContext.Routes.FirstOrDefaultAsync(routeDb => routeDb.id == route.id);
            routeDb.status = StatusRoute.PLANNED;
            await _dbContext.SaveChangesAsync();
            return routeDb;
        }
    }
}