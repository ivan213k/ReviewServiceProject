using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiEnums;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.PersonalReviews
{
    public partial class PersonalReviewPage
    {
        private ReviewEvaluationApiModel reviewEvaluation;
        private EvaluationJsonApiModel evaluationJson;
        private ReviewSessionApiModel reviewSession;
        private EvaluationPointsTemplateApiModel evaluationPointsTemplate;

        private EvaluationAreaApiModel currentEvaluationArea;
        private int currentAreaIndex = 1;
        private EvaluationAreaItemApiModel selectedAreaItemRow;
        public PersonalReviewPage()
        {
            evaluationJson = new EvaluationJsonApiModel();
            evaluationJson.Areas = new List<EvaluationAreaApiModel>();
            currentEvaluationArea = new EvaluationAreaApiModel();
            currentEvaluationArea.AreaItems = new List<EvaluationAreaItemApiModel>();
        }
        [Parameter]
        public Guid ReviewEvaluationGuid { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            reviewEvaluation = await HttpClient.GetFromJsonAsync<ReviewEvaluationApiModel>($"api/PersonalReview/{ReviewEvaluationGuid}");
            reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{reviewEvaluation.ReviewSessionId}");
            evaluationPointsTemplate = await HttpClient.GetFromJsonAsync<EvaluationPointsTemplateApiModel>($"api/EvaluationPoint/{reviewSession.EvaluationPointsTemplateId}");
            ApplicationState.SetState($"{reviewSession.Name} - {reviewSession.PersonUnderReview}", CreateFooterButtons());
            InitializeEvaluationJson();
        }
        private List<FooterButton> CreateFooterButtons()
        {
            List<FooterButton> buttons = new List<FooterButton>()
            {
                new FooterButton("Save", OnSaveClicked),
                new FooterButton("Publish", OnPublishClicked)
            };
            return buttons;
        }
        private async void OnSaveClicked()
        {
            if (reviewEvaluation.Status == ReviewEvaluationStatusApiEnum.Finished)
            {
                await DialogService.ShowMessageBox("Information", "Review already published!");
                return;
            }
            reviewEvaluation.Evaluation_json = JsonConvert.SerializeObject(evaluationJson);
            reviewEvaluation.Status = ReviewEvaluationStatusApiEnum.InProgress;
            await HttpClient.PutAsJsonAsync("/api/PersonalReview", reviewEvaluation);
            await DialogService.ShowMessageBox("Information", "Review saved successfully!");
        }
        private async void OnPublishClicked()
        {
            if (reviewEvaluation.Status == ReviewEvaluationStatusApiEnum.Finished)
            {
                await DialogService.ShowMessageBox("Information", "Review already published!");
                return;
            }
            reviewEvaluation.Evaluation_json = JsonConvert.SerializeObject(evaluationJson);
            reviewEvaluation.Status = ReviewEvaluationStatusApiEnum.Finished;
            await HttpClient.PutAsJsonAsync("/api/PersonalReview", reviewEvaluation);
            await DialogService.ShowMessageBox("Information", "Review published successfully!");
        }
        private void InitializeEvaluationJson()
        {
            if (!string.IsNullOrEmpty(reviewEvaluation.Evaluation_json))
            {
                evaluationJson = JsonConvert.DeserializeObject<EvaluationJsonApiModel>(reviewEvaluation.Evaluation_json);
            }
            currentEvaluationArea = evaluationJson.Areas.FirstOrDefault();
            selectedAreaItemRow = currentEvaluationArea.AreaItems.FirstOrDefault();
        }

        private void NextPage()
        {
            if (currentAreaIndex < evaluationJson.Areas.Count)
            {
                currentAreaIndex++;
                currentEvaluationArea = evaluationJson.Areas[currentAreaIndex - 1];
            }  
        }
        private void PreviousPage()
        {
            if (currentAreaIndex > 1)
            {
                currentAreaIndex--;
                currentEvaluationArea = evaluationJson.Areas[currentAreaIndex - 1];
            }
        }
        private void SelectedAreaChanged(HashSet<EvaluationAreaApiModel> sectedAreas)
        {
            currentAreaIndex = evaluationJson.Areas.IndexOf(currentEvaluationArea) + 1;
        }
    }
}
