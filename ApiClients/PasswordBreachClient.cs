using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace sample_application.ApiClients
{
    public class PasswordBreachClient : IPasswordBreachClient
    {
        private readonly HttpClient _httpClient;

        public PasswordBreachClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> HasPasswordBreached(string substringHashedPassword, string fullPassword)
        {
            try
            {
                var response = await _httpClient.GetAsync(substringHashedPassword);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("There was an error getting breached password list.");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var splitResponse = responseData.Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                bool breach = false;
                foreach (var hash in splitResponse)
                {
                    if ((substringHashedPassword + hash).ToLower().Contains(fullPassword.ToLower()))
                    {
                        breach = true;
                        break;
                    }
                }

                return breach;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
