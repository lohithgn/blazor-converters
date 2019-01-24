using System.Net.Http;
using System.Threading.Tasks;
using BlazorConverters.Shared;
using Microsoft.AspNetCore.Blazor;

namespace BlazorCalculator.Services
{
    public class ForexApiClient
    {
        private string key = "#{ApiKey}#";
        private string latestRatesUri = "http://apilayer.net/api/live";
        private readonly HttpClient _httpClient;

        public ForexApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Forex> LatestRates()
        {
            return await _httpClient.GetJsonAsync<Forex>($"{latestRatesUri}?access_key={key}");
        }
    }
}
