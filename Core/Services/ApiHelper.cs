using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace Core.Services
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
    /// <summary>
    /// helper method to set up the correct headers for a request to the IEX Api
    /// </summary>
    public class ApiHelper : IApiHelper
    {
        public HttpClient ApiClient { get; }

        public ApiHelper(IConfiguration configuration)
        {
            // need to put keys in the appsetting.Development.json file
            var iexToken = configuration["iex_Keys:SandBoxKey"];
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", iexToken);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}