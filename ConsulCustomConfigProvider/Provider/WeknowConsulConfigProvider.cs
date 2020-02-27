using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulCustomConfigProvider.Provider
{
    public class WeknowConsulConfigProvider : ConfigurationProvider
    {
        public override void Set(string key, string value)
        {
            base.Set(key, value);
        }

        public override bool TryGet(string key, out string value)
        {
            return base.TryGet(key, out value);
        }
    }
}
