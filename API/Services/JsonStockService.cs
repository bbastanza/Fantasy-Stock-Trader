using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.Extensions.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace API.Services
{
    public interface IJsonStockService
    {
        StockModel GetStockByName(string stockName);
    }
    public class JsonStockService : IJsonStockService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public JsonStockService(IApiHelper apiHelper, IConfiguration configuration)
        {
            _apiKey = configuration["iexKeys:TestKey"];
            _client = apiHelper.ApiClient;
        }

        public StockModel GetStockByName(string stockName)
        {
            var url =
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token={_apiKey}";

            var stockResponse = GetDataFromIex(url);
            Console.WriteLine("not serialized => \n"+stockResponse.Result);
            Console.WriteLine("");
            var serializedResponse = JsonSerializer.Deserialize<StockModel>(stockResponse.Result);
            Console.WriteLine("serialized => \n" + serializedResponse);
            return serializedResponse;
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