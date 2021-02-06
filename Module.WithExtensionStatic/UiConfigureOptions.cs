using Microsoft.AspNetCore.Hosting;

namespace Module.WithExtensionStatic
{
    public sealed class UiConfigureOptions : BaseModuleUiConfigureOptions
    {
        public UiConfigureOptions(IWebHostEnvironment environment)
            : base(environment)
        {
        }
    }
}