using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using ReviewService.Blazor.Client.ApiModelsExtensions;
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
        private EvaluationJson evaluationJson;
        private ReviewSessionApiModel reviewSession;
        private EvaluationPointApiModel midEvaluationPoint;
        private EvaluationPointsTemplateApiModel evaluationPointsTemplate;

        private EvaluationArea currentEvaluationArea;
        private int currentAreaIndex = 1;

        public PersonalReviewPage()
        {
            evaluationJson = new EvaluationJson();
            evaluationJson.Areas = new List<EvaluationArea>();
            currentEvaluationArea = new EvaluationArea();
            currentEvaluationArea.AreaItems = new List<EvaluationAreaItem>();
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
            ApplicationState.SetState("Personal Review", CreateFooterButtons());
            reviewEvaluation = await HttpClient.GetFromJsonAsync<ReviewEvaluationApiModel>($"api/PersonalReview/{ReviewEvaluationGuid}");
            reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{reviewEvaluation.ReviewSessionId}");
            evaluationPointsTemplate = await HttpClient.GetFromJsonAsync<EvaluationPointsTemplateApiModel>($"api/EvaluationPoint/{reviewSession.EvaluationPointsTemplateId}");
            midEvaluationPoint = evaluationPointsTemplate.EvaluationPoints.FirstOrDefault(e => e.Id == reviewSession.MidEvaluationPointId);
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
            reviewEvaluation.Evaluation_json = evaluationJson.ConvertToJsonString();
            reviewEvaluation.Status = ReviewEvaluationStatusApiEnum.InProgress;
            await HttpClient.PutAsJsonAsync("/api/PersonalReview", reviewEvaluation);
            await DialogService.ShowMessageBox("Information", "Review saved successfully!");
        }
        private async void OnPublishClicked()
        {
            reviewEvaluation.Evaluation_json = evaluationJson.ConvertToJsonString();
            reviewEvaluation.Status = ReviewEvaluationStatusApiEnum.Finished;
            await HttpClient.PutAsJsonAsync("/api/PersonalReview", reviewEvaluation);
            await DialogService.ShowMessageBox("Information", "Review published successfully!");
        }
        private void InitializeEvaluationJson()
        {
            if (string.IsNullOrEmpty(reviewEvaluation.Evaluation_json))
            {
                var areas = JsonConvert.DeserializeObject<List<AreaApiModel>>(reviewSession.Session_json);
                evaluationJson.Areas = areas.ConvertToEvaluationAreas();
                evaluationJson.MidEvaluationPoint = midEvaluationPoint.Name;
            }
            else
            {
                evaluationJson = JsonConvert.DeserializeObject<EvaluationJson>(reviewEvaluation.Evaluation_json);
            }
            currentEvaluationArea = evaluationJson.Areas.FirstOrDefault();
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
    }
}
