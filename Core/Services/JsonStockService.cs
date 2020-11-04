using Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Core.Services
{
    public interface IJsonStockService
    {
        StockModel GetStockByName(string stockName);
    }
    public class JsonStockService : IJsonStockService
    {
        private readonly HttpClient _client;

        public JsonStockService(IApiHelper apiHelper)
        {
            _client = apiHelper.ApiClient;
        }

        public StockModel GetStockByName(string stockName)
        {

            var url =
                // need to insert token in token=<TOKEN>
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token=";
            var stockResponse = GetDataFromIex(url);
            return new StockModel();
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