using System.Net.Http;
using System.Threading.Tasks;

namespace PollySample.interfaces
{
    public interface IHttpClientHandler
    {
        Task<HttpResponseMessage> GetAsJsonAsync(string url);
    }
}
