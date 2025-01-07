using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace MyBlazorApp.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
            Timeout = TimeSpan.FromSeconds(30)
        });

        builder.Services.AddAuthorizationCore();
        //builder.Services.AddScoped<TokenAuthStateProvider>();

        builder.Services.AddMudServices();

        await builder.Build().RunAsync();
    }
}