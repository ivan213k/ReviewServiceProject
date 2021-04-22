using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionViewTableFullScrean
    {
        private List<FinalReviewAreaApiModel> finalReviewAreas;
        private EvaluationPointsTemplateApiModel evaluationPointsTemplate;
       
        [Parameter]
        public int SessionId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{SessionId}");
            finalReviewAreas = await HttpClient.GetFromJsonAsync<List<FinalReviewAreaApiModel>>($"api/PersonalReviewView/{SessionId}");
            evaluationPointsTemplate = await HttpClient.GetFromJsonAsync<EvaluationPointsTemplateApiModel>($"api/EvaluationPoint/{reviewSession.EvaluationPointsTemplateId}");
            ApplicationState.SetState("", CreateFooterButtons());
        }

        private List<FooterButton> CreateFooterButtons()
        {
            return new List<FooterButton>()
            {
                new FooterButton("Back to review session", BackToSessionClicked),
                new FooterButton("Publish", SaveClicked)
            };
        }
        private async void SaveClicked() 
        {
            var result = await HttpClient.PutAsJsonAsync($"api/PersonalReviewView/{SessionId}", finalReviewAreas);
            if (result.IsSuccessStatusCode)
            {
                await DialogService.ShowMessageBox("Information", "Final reviews published successfully!");
            }
        }
        private void BackToSessionClicked() 
        {
            NavigationManager.NavigateTo($"/reviewSessionEdit/{SessionId}/{1}");
        }
    }
}
