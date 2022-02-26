using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PollySample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Policies>();
                    services.AddHostedService<Worker>();
                    services.AddSingleton<interfaces.IHttpClientHandler, interfaces.HttpClientHandler>();
                });
    }
}