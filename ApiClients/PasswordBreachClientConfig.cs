using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_application.ApiClients
{
    public class PasswordBreachClientConfig : IPasswordBreachClientConfig
    {
        public PasswordBreachClientConfig(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Bind("PasswordBreach", this);
        }

        public Uri BaseUrl { get; set; }

        public int Timeout { get; set; }
    }
}
