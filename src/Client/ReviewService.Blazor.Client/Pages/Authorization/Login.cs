using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Services.Interfaces;
using ReviewService.Shared.AuthorizationDtos;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Authorization
{
    public partial class Login
    {
        private UserForAuthenticationDto _userForAuthentication = new UserForAuthenticationDto();
   
        [Inject]
        public NavigationManager NavigationManager { get; set; }
       
        [Inject]
        public IAuthorizationService AuthorizationService { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }
        public async Task ExecuteLogin()
        {
            var result = await AuthorizationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Error = result.ErrorMessage;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
