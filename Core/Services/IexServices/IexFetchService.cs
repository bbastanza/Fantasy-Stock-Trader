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
    }

    public class IexFetchService : IIexFetchService
    {
        private readonly HttpClient _client;
        private readonly string _path;
        private readonly IConfiguration _configuration;

        public IexFetchService(IApiHelper apiHelper, IConfiguration configuration)
        {
            _configuration = configuration;
            _client = apiHelper.ApiClient;
            _path = Path.GetFullPath(ToString());
        }

        public IexStock GetStockBySymbol(string stockName)
        {
            // Production
            var iexType = "cloud";
            var apiKey = _configuration["IexKeys:PublicKey"];
            
            // Development
            // var iexType =  "sandbox";
            // var apiKey = _configuration["IexKeys:TestKey"]; 
            
            var url =
                $"https://{iexType}.iexapis.com/stable/stock/{stockName}/quote?token={apiKey}";

            try
            {
                var stockResponse = GetDataFromIex(url);
                return JsonSerializer.Deserialize<IexStock>(stockResponse.Result);
            }
            catch
            {
                throw new IexException(_path, "GetStockBySymbol()");
            }
        }

        private async Task<string> GetDataFromIex(string url)
        {
            var response = await _client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }
    }
}