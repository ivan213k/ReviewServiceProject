using Microsoft.AspNetCore.Components;
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
        public ReviewSessionViewTableFullScrean()
        {

        }
        [Parameter]
        public int SessionId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

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
                new FooterButton("Save", SaveClicked)
            };
        }
        private async void SaveClicked() 
        {
            await HttpClient.PutAsJsonAsync($"api/PersonalReviewView/{SessionId}", finalReviewAreas);
        }
        private void BackToSessionClicked() 
        {
            NavigationManager.NavigateTo($"/reviewSessionEdit/{SessionId}");
        }
    }
}
