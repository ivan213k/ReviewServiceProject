using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.Users
{
    public partial class UsersIndex
    {
        private List<UserApiModel> _users;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Users", new List<FooterButton>());
            _users = await HttpClient.GetFromJsonAsync<List<UserApiModel>>("api/Users");
        }

    }
}
