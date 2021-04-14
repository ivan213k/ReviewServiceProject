using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Services.Interfaces;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Authorization
{
    public partial class Logout
    {
        [Inject]
        public IAuthorizationService AuthenticationService { get; set; }
       
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
