using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollySample.interfaces
{
    public class HttpClientHandler : IHttpClientHandler
    {
        private readonly Lazy<HttpClient> _httpClient = new(() =>
        {
            var c = new HttpClient()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
                Timeout = TimeSpan.FromSeconds(3)
            };

            c.DefaultRequestHeaders.Add("Accept", "application/json");
            return c;
        });

        public async Task<HttpResponseMessage> GetAsJsonAsync(string url = "/todos/1")
        {

            var response = await _httpClient.Value.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var customerJsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(customerJsonString);
            }
            else
            {
                Console.WriteLine("request failed {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            return response;
        }
    }
}
