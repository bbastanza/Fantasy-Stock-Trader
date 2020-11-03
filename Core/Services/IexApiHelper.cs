using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Services
{
    public interface IIexApiHelper
    {
        HttpClient ApiClient { get; }
    }
    /// <summary>
    /// helper method to set up the correct headers for a request to the IEX Api
    /// </summary>
    public class IexApiHelper : IIexApiHelper
    {
        public HttpClient ApiClient { get; }

        public IexApiHelper()
        {
            // need to put keys in the appsetting.Development.json file
            var iexToken = "sting";
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", iexToken);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}