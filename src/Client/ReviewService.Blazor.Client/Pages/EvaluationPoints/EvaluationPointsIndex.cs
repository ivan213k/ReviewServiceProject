using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewService.Shared.ApiModels;
using ReviewService.Blazor.Client.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.State;
using ReviewService.Blazor.Client.Layout.Footer;
using MudBlazor;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class EvaluationPointsIndex
    {
        private List<EvaluationPointsTemplateApiModel> _evaluationpoints;

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
            ApplicationState.SetState("Evaluation Points", SetButtons());
            _evaluationpoints = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
        }

        private List<FooterButton> SetButtons()
        {
            List<FooterButton> buttons = new List<FooterButton>();
            buttons.Add(new FooterButton("Add Evaluation", OnAddEvaluationClicked));
            return buttons;
        }

        private void OnAddEvaluationClicked()
        {
            NavigationManager.NavigateTo("/evaluationpoints/add");
        }

        private void OnEditClicked(int id)
        {
            NavigationManager.NavigateTo($"/evaluationpoints/edit/{id}");
        }

        private async void OnDeleteClicked(EvaluationPointsTemplateApiModel template)
        {
            var message = $"Actually delete\"{template.Name}\" evaluation points template?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteEvaluationPointsTemplate(template);
            }
        }
        private async void DeleteEvaluationPointsTemplate(EvaluationPointsTemplateApiModel template)
        {
            await HttpClient.DeleteAsync($"api/EvaluationPoint/{template.Id}");
            _evaluationpoints = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
            StateHasChanged();
        }

    }
}
