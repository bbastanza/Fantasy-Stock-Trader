using System.Net.Http;
using System.Net.Http.Headers;

namespace Core.Services.IexServices
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; set; }
    }

    public class ApiHelper : IApiHelper
    {
        public HttpClient ApiClient { get; set; }

        public ApiHelper()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}