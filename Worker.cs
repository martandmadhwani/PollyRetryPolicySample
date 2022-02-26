using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PollySample.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
