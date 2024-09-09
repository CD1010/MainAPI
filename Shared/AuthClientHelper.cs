using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{

    /// <summary>
    /// helper class for common client functions
    /// </summary>
    public class AuthClientHelper : IAuthClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        /// <summary>
        /// cto
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClientFactory"></param>
        public AuthClientHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Creates a client, if the key passed is correct
        /// </summary>
        /// <param name="providedApiKey">providedKey to use</param>
        /// <returns>null, or the Client</returns>
        public HttpClient? GetAuthorizedHttpClient(string providedApiKey)
        {
            var apiKey = _configuration["ApiKey"];

            if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != apiKey)
            {
                return null;
            }

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            return httpClient;
        }
    }
}
