using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace WebApplication3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var startUp = Assembly.Load(new AssemblyName("Module.One"));
            System.Runtime.Loader.AssemblyLoadContext.Default
                            .LoadFromAssemblyName(new AssemblyName("Module.One"))
                            .GetTypes()
                            .Where(type => typeof(IModule).IsAssignableFrom(type))
                            .ForEach(module =>
                            {
                                (System.Activator.CreateInstance(module) as IModule)
                                    .ConfigureServices(services);
                            });
            System.Runtime.Loader.AssemblyLoadContext.Default
                            .LoadFromAssemblyName(new AssemblyName("Module.mvc"))
                            .GetTypes()
                            .Where(type => typeof(IModule).IsAssignableFrom(type))
                            .ForEach(module =>
                            {
                                (System.Activator.CreateInstance(module) as IModule)
                                    .ConfigureServices(services);
                            });
            //var exter = System.Runtime.Loader.AssemblyLoadContext.Default
            //                  .LoadFromAssemblyName(new AssemblyName("Module.mvc"));

            var assembly = typeof(Module.mvc.Areas.Products.Controllers.HomeController).Assembly;

            //services.AddModules();
            services.AddControllersWithViews()
                .AddApplicationPart(assembly)
                .AddRazorRuntimeCompilation();

            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            { options.FileProviders.Add(new EmbeddedFileProvider(assembly)); });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "MyArea",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}

