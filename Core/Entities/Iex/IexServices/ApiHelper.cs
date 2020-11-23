using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Entities.Iex.IexServices
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

        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}