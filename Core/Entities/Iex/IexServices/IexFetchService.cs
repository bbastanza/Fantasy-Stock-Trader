using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Core.Entities.Iex.IexServices
{
    public interface IIexFetchService
    {
        IexStockModel GetStockBySymbol(string stockName);
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

        public IexStockModel GetStockBySymbol(string stockName)
        {
            if (stockName == null) 
                throw new InvalidDataException("A symbol has not bee provided for this search");

            var url =
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token={_apiKey}";

            var stockResponse = GetDataFromIex(url);
            return JsonSerializer.Deserialize<IexStockModel>(stockResponse.Result);
        }

        private async Task<string> GetDataFromIex(string url)
        {
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            throw new ExternalException("There was a problem receiving data from the IEX API");
        }
    }
}