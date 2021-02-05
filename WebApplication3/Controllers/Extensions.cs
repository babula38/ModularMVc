
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Module.One;
using System.Linq;
using System.Reflection;

namespace WebApplication3.Controllers
{
    public static class Extensions
    {
        public static void AddModules(this IServiceCollection services)
        {
            var all = Assembly
                    .GetEntryAssembly()
                    .GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .SelectMany(x => x.GetTypes())
                    .Where(type => typeof(IModule).IsAssignableFrom(type));
            //.Where(t => t.IsClass);

            //all.ForEach(module =>
            //{
            //    (System.Activator.CreateInstance(module) as IModule)
            //                     .ConfigureServices(services);
            //});
        }
    }
}

