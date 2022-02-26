# PollyRetryPolicySample
Apply retry policy using Polly

** Add nuget package **
- Microsoft.Extensions.Http.Polly - used version 6.0.2
- Polly - used version 7.2.3

## Create class Policies for policies.

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

## In configure service register Policies

** services.AddSingleton<Policies>();**

## Inject polly policies as dependacy injection

namespace PollySample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientHandler _sampleCall;
        private readonly Policies _policies;

        public Worker(ILogger<Worker> logger, IHttpClientHandler sampleCall, Policies policies)
        {
            _logger = logger;
            _sampleCall = sampleCall;
            _policies = policies;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _policies.RequestTimeoutPolicy.ExecuteAsync(() => _sampleCall.GetAsJsonAsync("/todos1/1"));
               
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000000, stoppingToken);
            }
        }
    }
}
