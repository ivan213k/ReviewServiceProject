using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiEnums;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionsPage
    {
        private List<ReviewSessionApiModel> reviewSessions;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Review Sessions", CreateFooterButtons());
            reviewSessions = await HttpClient.GetFromJsonAsync<List<ReviewSessionApiModel>>("api/ReviewSession");
        }

        private List<FooterButton> CreateFooterButtons()
        {
            var buttons = new List<FooterButton>
            {
                new FooterButton("Add new", AddNewSessionClicked)
            };
            return buttons;
        }

        private int GetFinishedReviwersCount(ReviewSessionApiModel session) 
        {
            return session.ReviewEvaluations.Where(e => e.Status == ReviewEvaluationStatusApiEnum.Finished).Count();
        }

        private void AddNewSessionClicked()
        {
            NavigationManager.NavigateTo("/selectReviewTemplate");
        }
        private void OnViewClicked(int reviewSessionId)
        {
            NavigationManager.NavigateTo($"/reviewSessionEdit/{reviewSessionId}");
        }
        private async void DeleteSessionClicked(ReviewSessionApiModel reviewSession)
        {
            var message = $"Actually delete\"{reviewSession.Name}\" session ?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteReviewSession(reviewSession);
            }
        }
        private async void DeleteReviewSession(ReviewSessionApiModel reviewSession)
        {
            await HttpClient.DeleteAsync($"api/ReviewSession/{reviewSession.Id}");
            reviewSessions = await HttpClient.GetFromJsonAsync<List<ReviewSessionApiModel>>("api/ReviewSession");
            StateHasChanged();
        }
    }
}
