using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor.Services;
using ReviewService.Blazor.Client.State;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ReviewService.Blazor.Client.AuthProviders;
using ReviewService.Blazor.Client.Services.Interfaces;
using ReviewService.Blazor.Client.Services.AuthorizationServices;

namespace ReviewService.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<ApplicationState>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();

            await builder.Build().RunAsync();
        }
    }
}
