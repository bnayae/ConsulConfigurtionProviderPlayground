﻿using System;
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
    public class DemoController : ControllerBase
    {
        private readonly ILogger<SampleController> _logger;
        private readonly IOptionsSnapshot<DemoAppSettings> _optionsSnapshot;

        public DemoController(
            ILogger<SampleController> logger,
            IOptionsSnapshot<DemoAppSettings> optionsSnapshot)
        {
            _logger = logger;
            _optionsSnapshot = optionsSnapshot;
        }

        // GET api/values  
        [HttpGet]
        public async Task<string> GetAsync()
        {
            await Task.Delay(400);
            return $"IOptionsSnapshot: {_optionsSnapshot.Value.Color}" ;
        }
    }
}
