using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class EvaluationPointEdit
    {
        private EvaluationPointsTemplateApiModel _evaluationPointsTemplate;
        private EvaluationPointApiModel _evaluationPoint;
        private List<FooterButton> _buttons;

        [Parameter]
        public int Id { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public EvaluationPointEdit()
        {
            _evaluationPoint = new EvaluationPointApiModel();
            _evaluationPointsTemplate = new EvaluationPointsTemplateApiModel();
            _evaluationPointsTemplate.EvaluationPoints = new List<EvaluationPointApiModel>();
            _buttons = new List<FooterButton>();
        }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Evaluation Point Edit", SetButtons());
            _evaluationPointsTemplate = await HttpClient.GetFromJsonAsync<EvaluationPointsTemplateApiModel>($"api/EvaluationPoint/{Id}");
        }

        private List<FooterButton> SetButtons()
        {
            List<FooterButton> buttons = new List<FooterButton>();
            buttons.Add(new FooterButton("Save", OnSaveClicked));
            buttons.Add(new FooterButton("Cancel", OnCancelClicked));
            return buttons;
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
            await HttpClient.PutAsJsonAsync("api/EvaluationPoint", _evaluationPointsTemplate);
            NavigationManager.NavigateTo("/evaluationpoints");
        }

        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/evaluationpoints");
        }
    }
}
