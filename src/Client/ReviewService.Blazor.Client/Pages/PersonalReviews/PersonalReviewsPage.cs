using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.PersonalReviews
{
    [Authorize (Roles = "Administrator,Manager,Reviewer")]
    public partial class PersonalReviewsPage
    {
        private List<PersonalReviewApiModel> personalReviews;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Available reviews", new List<FooterButton>());
            personalReviews = await HttpClient.GetFromJsonAsync<List<PersonalReviewApiModel>>("api/PersonalReview");
        }
        private void NavigateToReviewPage(string link) 
        {
            NavigationManager.NavigateTo(link);
        }
    }
}
