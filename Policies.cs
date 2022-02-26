using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;

public class Policies
{
    public readonly AsyncRetryPolicy<HttpResponseMessage> RequestTimeoutPolicy;
    public Policies()
    {
        RequestTimeoutPolicy = Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.NotFound)
         .Or<Exception>()
          .WaitAndRetryAsync(3, retryCount => TimeSpan.FromSeconds(10), (result, timeSpan, retryCount, context) =>
           {
               Console.WriteLine("retry");
           });

    }
}