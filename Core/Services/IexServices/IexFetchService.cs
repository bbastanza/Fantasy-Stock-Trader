using System.IO;
using System.Net.Http;
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
        void UpdateHolding(Holding holding);
    }

    public class IexFetchService : IIexFetchService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;
        private readonly string _path;

        public IexFetchService(IApiHelper apiHelper, IConfiguration configuration)
        {
            _apiKey = configuration["iexKeys:TestKey"];
            _client = apiHelper.ApiClient;
            _path = Path.GetFullPath(ToString());
        }

        public IexStock GetStockBySymbol(string stockName)
        {
            var url =
                $"https://sandbox.iexapis.com/stable/stock/{stockName}/quote?token={_apiKey}";

            try
            {
                var stockResponse = GetDataFromIex(url);
                return JsonSerializer.Deserialize<IexStock>(stockResponse.Result);
            }
            catch
            {
                throw new IexException(_path, "GetStockBySymbol");
            }
        }

        public void UpdateHolding(Holding holding)
        {
            if (holding.TotalShares == 0) return;
            var stockData = GetStockBySymbol(holding.Symbol);
            holding.SetValue(stockData.LatestPrice);
        }

        private async Task<string> GetDataFromIex(string url)
        {
            var response = await _client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}