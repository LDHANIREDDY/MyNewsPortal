using MyNewsPortals.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MyNewsPortals.Services
{
    public class NewsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyNewsPortalApp/1.0");
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
        }

        public async Task<NewsApiResponse> GetTopHeadlinesByCountryAsync(string countryCode)
        {
            string url = $"https://newsapi.org/v2/everything?q={Uri.EscapeDataString(countryCode)}&sortBy=publishedAt&pageSize=10&language=en";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"NewsAPI Error: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var newsResponse = JsonConvert.DeserializeObject<NewsApiResponse>(json);

            if (newsResponse == null)
            {
                throw new Exception("Failed to deserialize NewsAPI response.");
            }

            return newsResponse;
        }
    }
}
