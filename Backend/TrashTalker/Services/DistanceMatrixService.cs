using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrashTalker.Helpers;
using TrashTalker.Models;
using static TrashTalker.Config;

namespace TrashTalker.Services
{
    /// <summary>
    /// Class that allows to get information about distances between <see cref="RecycleBin"/> 
    /// </summary>
    public class DistanceMatrixService : IDistanceMatrixService
    {
        /// <summary>
        /// To create <see cref="HttpClient"/>  instances
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        public DistanceMatrixService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        /// <inheritdoc />
        public async Task<DistanceMatrix> getDistanceMatrix(List<RecycleBin> recycleBins)
        {
            var client = _httpClientFactory.CreateClient();

            //create a list of coordinates
            var listCoordinates =
                new List<string>(recycleBins.Select(bin => $"{bin.latit}{LAT_LONG_DELIMITATOR}{bin.longit}")).Prepend(COMPANY_LOCATION_GOOGLE_API);

            //single string with the coordinates
            var originsDestinations = string.Join($"{CORDINATOR_DELIMITATOR}", listCoordinates);

            var uri = new Uri($"{GOOGLE_API_URL}?region=pt&origins={originsDestinations}&destinations={originsDestinations}&key={GOOGLE_API_KEY}");

            var response = await client.GetFromJsonAsync<DistanceMatrix>(uri);

            return response;
        }

        /// <inheritdoc />
        public async Task<(int distance, int duration)> getEstimatedDurationAndDistance(Route route)
        {
            var distanceMatrix = await getDistanceMatrix(route.collectPoints.Select(s=> s.recycleBin).ToList());

            var distance = 0;
            var duration = 0;

            //Example by indexes
            // 0 to 1 + 1 to 2 + 2 to 3 + 3 to 4 .... + N - 0
            var index = 0;
            while (index < distanceMatrix.rows.Count - 1)
            {
                var info = distanceMatrix.rows.ElementAt(index).elements.ElementAt(index + 1);
                distance += info.distance.value;
                duration += info.duration.value;
                index++;
            }

            //Some the return to the start point
            var returnInfo = distanceMatrix.rows.ElementAt(index).elements.ElementAt(0);
            distance += returnInfo.distance.value;
            duration += returnInfo.duration.value;

            return (distance, duration);
        }
    }

    public interface IDistanceMatrixService
    {
        /// <summary>
        /// Determines distance matrix for an specific <paramref name="route"/>  
        /// </summary>
        /// <param name="route">Route where the distance matrix will be calculated</param>
        /// <returns><see cref="DistanceMatrix{T}" /> with distance and duration of this <paramref name="route"/></returns>
        Task<DistanceMatrix> getDistanceMatrix(List<RecycleBin> recycleBins);

        ///  <summary>
        ///  Determines duration and distance for an specific <paramref name="route"/>  
        ///  </summary>
        ///  <param name="route">Route where the distance and duration will be calculated</param>
        ///  <returns><see cref="Dictionary"/> of <see cref="String" />, <see cref="Int32" />  with distance and duration between the locations </returns>
        Task<(int distance, int duration)> getEstimatedDurationAndDistance(Route route);
    }
}