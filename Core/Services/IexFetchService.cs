using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Core.Services
{
    public interface IIexFetchService
    {
        StockModel GetStockByName(string stockName);
    }
    public class IexFetchService : IIexFetchService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public IexFetchService(IApiHelper apiHelper, IConfiguration configuration)
        {
            _apiKey = configuration["iexKeys:TestKey"];
            _client = apiHelper.ApiClient;
        }

        public StockModel GetStockByName(string stockName)
        {
            var url =
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token={_apiKey}";

            var stockResponse = GetDataFromIex(url);
           
            return JsonSerializer.Deserialize<StockModel>(stockResponse.Result);
        }

        private async Task<string> GetDataFromIex(string url)
        {
           var response = await _client.GetAsync(url);
           
           if (response.IsSuccessStatusCode)
               return await response.Content.ReadAsStringAsync();
            
           throw new Exception("error in JsonStockService");
        }
    }
}