using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Winton.Extensions.Configuration.Consul;
using static System.Text.Encoding;

// credit: https://www.c-sharpcorner.com/article/dynamic-asp-net-core-configurations-with-consul-kv/
// credit: https://github.com/wintoncode/Winton.Extensions.Configuration.Consul

namespace ConsulConfigurtionProviderSample
{
    public class Startup
    {
        public Startup(IHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;

            //string consoleUrl = configuration["Consul:Host"];

            //var builder = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            //.AddConsul(
            //            $"{env.ApplicationName}/{env.EnvironmentName}/appsettings.json",
            //            (IConsulConfigurationSource options) =>
            //            {
            //                options.ConsulConfigurationOptions =
            //                    cco =>
            //                    {
            //                        cco.Address = new Uri(consoleUrl);
            //                    };
            //                options.Optional = true;
            //                options.PollWaitTime = TimeSpan.FromSeconds(5);
            //                options.ReloadOnChange = true;
            //                options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
            //            });
            //Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddConsulInjection(Configuration); // custom implementation which AddSingleton<IConsulClient>
            services.Configure<DemoAppSettings>(Configuration);
            services.Configure<DemoAppSettings>(Configuration.GetSection(nameof(DemoAppSettings)));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
