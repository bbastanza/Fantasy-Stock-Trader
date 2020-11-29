using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Services.IexServices
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }

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