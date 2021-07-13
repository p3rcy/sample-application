using System;

namespace sample_application.ApiClients
{
    public interface IPasswordBreachClientConfig
    {
        Uri BaseUrl { get; set; }
        int Timeout { get; set; }
    }
}