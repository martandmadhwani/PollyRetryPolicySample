using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollySample.interfaces
{
   public interface IHttpClientHandler
{
    Task<HttpResponseMessage> GetAsJsonAsync(string url);
}
}
