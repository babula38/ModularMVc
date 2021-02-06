using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.WithExtensionStatic
{
    public class Module : IModule
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOptions(typeof(UiConfigureOptions));
        }
    }
}
