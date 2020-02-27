using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConsulConfigurtionProviderSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly IConsulClient _consulClient;

        public SampleController(
            ILogger<SampleController> logger,
            IConsulClient consulClient)
        {
            _logger = logger;
            _consulClient = consulClient;
        }

        [HttpGet]
        public async Task<string> GetBAsync()
        {
            await Task.Delay(400);
            return "OK";
        }

        [HttpGet("{key}")]
        public async Task<string> GetByKeyAsync(string key)
        {
            await Task.Delay(400);
            //return new[] { _config.Color, _config.Size.ToString() };
            var decoded = "MISSING!";
            //query the value  
            var res = await _consulClient.KV.Get(key);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //convert byte[] to string  
                decoded = System.Text.Encoding.UTF8.GetString(res.Response.Value);
            }

            return $"value is: {decoded}";
        }
    }
}
