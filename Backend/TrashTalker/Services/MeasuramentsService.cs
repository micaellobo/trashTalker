using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrashTalker.Database.Repositories.MeasurementsRepository;
using TrashTalker.Dto.Measurement;
using TrashTalker.Helpers;
using TrashTalker.Models;
using static TrashTalker.Config;

namespace TrashTalker.Services
{
    public interface IMeasuramentsService
    {
        /// <summary>
        /// Gets the information about the last updated measuraments
        /// </summary>
        /// <returns>A List of <see cref="MeasuramentResponse"/></returns>
        Task<MeasuramentResponse> getDistancesFromArduinoCloud();
    }

    public class MeasuramentsService : IMeasuramentsService
    {
        /// <summary>
        /// To create <see cref="HttpClient"/> instances
        /// </summary>
        private readonly IHttpClientFactory _httpClientFactory;

        public MeasuramentsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> getToken()
        {
            var client = _httpClientFactory.CreateClient();
            var body = new Dictionary<string, string>
            {
                {"grant_type", ARDUINO_GRANT_TYPE},
                {"client_id", ARDUINO_CLIENT_ID},
                {"client_secret", ARDUINO_CLIENT_SECRET},
                {"audience", URL_ARDUINO_AUDIENCE}
            };

            var formUrlEncodedContent = new FormUrlEncodedContent(body);
            var response = await client.PostAsync(new Uri(URL_ARDUINO_TOKEN), formUrlEncodedContent);

            //Response colocada num dicionario
            var resValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());

            return resValues["access_token"];
        }

        /// <inheritdoc/>
        public async Task<MeasuramentResponse> getDistancesFromArduinoCloud()
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {await getToken()}");

            var uri = new Uri($"{URL_ARDUINO_AUDIENCE}/v2/dashboards?dashboard&{ARDUINO_CLIENT_ID}");

            var response = await client.GetFromJsonAsync<MeasuramentResponse[]>(uri);

            return response?[0];
        }
    }
}