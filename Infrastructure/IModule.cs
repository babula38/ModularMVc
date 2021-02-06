using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public interface IModule
    {
        void ConfigureServices(IServiceCollection collection);
    }
}
