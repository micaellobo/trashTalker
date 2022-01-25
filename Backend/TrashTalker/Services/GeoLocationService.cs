using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrashTalker.Helpers;
using static TrashTalker.Config;


namespace TrashTalker.Services
{
    /// <summary>
    /// Class that interacts with external APIs and gets information about addresses  
    /// </summary>
    public class GeoLocationService : IGeoLocationService
    {
        /// <summary>
        /// To create <see cref="HttpClient"/>  instances
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        public GeoLocationService(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;


        /// <inheritdoc />
        public async Task<GeoLocation[]> getGeoLocation(string zipCode)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var uri = new Uri($"{GEOLOCATION_API}{zipCode.Remove(4, 1)}");

                var response = await client.GetFromJsonAsync<GeoLocation[]>(uri);

                return response;
            }
            catch (Exception e )
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public interface IGeoLocationService
    {
        /// <summary>
        /// Gets the information about an specific <paramref name="zipcode"/>
        /// </summary>
        /// <param name="zipcode"> zipCode to find the associated locations</param>
        /// <returns> A List of <see cref="GeoLocation"/></returns>
        Task<GeoLocation[]> getGeoLocation(string zipcode);
    }
}