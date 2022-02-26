using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Net.Http;

namespace PollySample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        //static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        //{
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        //          //.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(10));
        //          .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
        //          {
        //              Console.WriteLine($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
        //          });
        //}

     
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
                    services.AddSingleton<Policies>();
                    services.AddHostedService<Worker>();
                    services.AddSingleton<interfaces.IHttpClientHandler, interfaces.HttpClientHandler>();
                    
                  
                });
    }
}
