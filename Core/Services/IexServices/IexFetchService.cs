using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Core.Services.IexServices
{
    public interface IIexFetchService
    {
        IexStock GetStockBySymbol(string stockName);
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

        public IexStock GetStockBySymbol(string stockName)
        {
            if (stockName == null)
                throw new InvalidInputException(Path.GetFullPath(ToString()),
                    "GetStockBySymbol()");

            var url =
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token={_apiKey}";

            var stockResponse = GetDataFromIex(url);
            return JsonSerializer.Deserialize<IexStock>(stockResponse.Result);
        }

        private async Task<string> GetDataFromIex(string url)
        {
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();

            throw new IexException(Path.GetFullPath(ToString()), "GetDataFromIex()");
        }
    }
}