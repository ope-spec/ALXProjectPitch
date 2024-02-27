

using Microsoft.Extensions.Options;
using ProjectPitch4.Models;

namespace ProjectPitch4.Services
{
    public class HttpServer
    {
        public HttpClient HttpClient { get; set; }
        public HttpServer(IOptions<AppConfig> config)
        {
            HttpClient = new HttpClient();
        }

        internal async Task<string> MakeGetRequestAsync(string httpBaseUrl)
        {
            return await HttpClient.GetStringAsync(httpBaseUrl);
        }
    }
}
