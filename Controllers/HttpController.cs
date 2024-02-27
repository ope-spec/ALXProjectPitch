using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectPitch4.Models;
using ProjectPitch4.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectPitch4.Controllers
{
    /// <summary>
    /// Controller for making HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HttpController : ControllerBase
    {
        private readonly HttpServer _httpServer;
        private readonly AppConfig _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpController"/> class.
        /// </summary>
        /// <param name="httpServer">The HTTP server service.</param>
        /// <param name="config">The application configuration options.</param>
        public HttpController(HttpServer httpServer, IOptions<AppConfig> config)
        {
            _httpServer = httpServer;
            _config = config.Value;

        }


        /// <summary>
        /// Sends an HTTP GET request to Google.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation, returning the response string.
        /// </returns>
        [HttpGet("GetGoogle")]
        public async Task<string> GetAsync()
        {
            return await _httpServer.MakeGetRequestAsync(_config.HttpBaseUrl);
        }

    }
}
