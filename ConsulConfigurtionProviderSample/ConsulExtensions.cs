using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsulConfigurtionProviderSample
{
    public static class ConsulExtensions
    {
        public static IServiceCollection AddConsul(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            string address = configuration["Consul:Host"];
            services.AddSingleton<IConsulClient, ConsulClient>(CreateConsul);
            return services;

            // local function
            ConsulClient CreateConsul(IServiceProvider serviceProvider)
            {
                var client = new ConsulClient(
                    (ConsulClientConfiguration consulConfig) => 
                            consulConfig.Address = new Uri(address),
                    (HttpClient httpClient) => { }, 
                    (HttpClientHandler handlerOverride) =>
                    {
                        //disable proxy of HttpcLientHandler  
                        handlerOverride.Proxy = null;
                        handlerOverride.UseProxy = false;
                    });
                return client;
            }
        }

    }
}
