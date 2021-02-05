using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Module.One
{
    public class Module : IModule
    {
        public void ConfigureServices(IServiceCollection collection)
        {
            collection.AddTransient<ITestService, TestService>();
        }
    }

    public interface IModule1
    {
        void ConfigureServices(IServiceCollection collection);
    }
}
