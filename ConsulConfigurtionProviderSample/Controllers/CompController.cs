using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsulConfigurtionProviderSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<DemoAppSettings> _options;
        private readonly IOptionsSnapshot<DemoAppSettings> _optionsSnapshot;

        public CompController(
            ILogger<SampleController> logger,
            IConfiguration configuration,
            IOptions<DemoAppSettings> options,
            IOptionsSnapshot<DemoAppSettings> optionsSnapshot)
        {
            _logger = logger;
            _configuration = configuration;
            _options = options;
            _optionsSnapshot = optionsSnapshot;
        }

        // GET api/values  
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            await Task.Delay(400);
            return new string[]
                    {
                        $"IConfiguration:   {_configuration["DemoAppSettings:Color"]}",
                        $"IOptions:         {_options.Value.Color}",
                        $"IOptionsSnapshot: {_optionsSnapshot.Value.Color}"
                    };
        }
    }
}
