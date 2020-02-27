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
using static System.Text.Encoding;

// credit: https://www.c-sharpcorner.com/article/dynamic-asp-net-core-configurations-with-consul-kv/
// credit: https://github.com/wintoncode/Winton.Extensions.Configuration.Consul

namespace ConsulConfigurtionProviderSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddConsul(Configuration);
            //services.Configure<AConfig>(Configuration);
            //services.Configure<AConfig>(async config => {
            //    using (var client = new ConsulClient(clientConfig => 
            //                    clientConfig.Address = new Uri("http://localhost:4000/")))
            //    {
            //        var getPair = await client.KV.Get("serviceUrl");
            //        if (getPair.Response != null)
            //        {
            //            var serviceUrl = UTF8.GetString(
            //                            getPair.Response.Value, 
            //                            0, 
            //                            getPair.Response.Value.Length);
            //            config.ServiceUrl = serviceUrl;
            //        }
            //    }
            //});
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
