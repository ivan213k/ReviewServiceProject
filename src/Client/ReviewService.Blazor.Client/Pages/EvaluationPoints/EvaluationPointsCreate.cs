using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.State;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class EvaluationPointsCreate
    {
        private EvaluationPointsTemplateApiModel _evaluationPointsTemplate;
        private EvaluationPointApiModel _evaluationPoint;

        [Inject]
        public ApplicationState ApplicationState { get; set; } 

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public EvaluationPointsCreate()
        {
            _evaluationPoint = new EvaluationPointApiModel();
            _evaluationPointsTemplate = new EvaluationPointsTemplateApiModel();
            _evaluationPointsTemplate.EvaluationPoints = new List<EvaluationPointApiModel>();
        }

        protected override void OnInitialized()
        {
            ApplicationState.SetHeaderTitle("Evaluation Point Add");
        }

        private void OnAddRowClicked()
        {
            _evaluationPointsTemplate.EvaluationPoints.Add(_evaluationPoint);
            _evaluationPoint = new EvaluationPointApiModel();
            StateHasChanged();
        }

        private void OnDeleteClicked(EvaluationPointApiModel point)
        {
            _evaluationPointsTemplate.EvaluationPoints.Remove(point);
        }

        private async void OnSaveClicked()
        {
            await HttpClient.PostAsJsonAsync("api/EvaluationPoint", _evaluationPointsTemplate);
            NavigationManager.NavigateTo("/evaluationpoints");
        }

        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/evaluationpoints");
        }


    }
}
