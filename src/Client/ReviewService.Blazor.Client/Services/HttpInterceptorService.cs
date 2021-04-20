using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;
using Toolbelt.Blazor;

namespace ReviewService.Blazor.Client.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly ISnackbar _snackbar;
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager, ISnackbar snackbar)
        {
            _interceptor = interceptor;
            _navManager = navManager;
            _snackbar = snackbar;
        }
        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;
        private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                switch (statusCode)
                {
                    case HttpStatusCode.BadRequest:
                        var content = await e.GetCapturedContentAsync();
                        var error = await content.ReadFromJsonAsync<ProblemDetails>();
                        _snackbar.Add($"{e.Response.ReasonPhrase}: {error.Detail}", Severity.Error);
                        break;
                    case HttpStatusCode.Unauthorized:
                        _navManager.NavigateTo("/login");
                        break;
                    default:
                        _snackbar.Add($"{e.Response.ReasonPhrase}", Severity.Error);
                        break;
                }
            }
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}
