using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Winton.Extensions.Configuration.Consul;

namespace ConsulConfigurtionProviderSample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {                
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(
                    builder =>
                    {
                        builder
                            .AddConsul(
                                $"app1/appsettings.json",
                                options =>
                                {
                                    options.ConsulConfigurationOptions =
                                        cco => { cco.Address = new Uri("http://127.0.0.1:8500"); };
                                    options.Optional = true;
                                    options.PollWaitTime = TimeSpan.FromSeconds(5);
                                    options.ReloadOnChange = true;
                                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                })
                            .AddEnvironmentVariables();
                    });
    }
}
