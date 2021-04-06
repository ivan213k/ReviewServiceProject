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
using ReviewService.Blazor.Client.Layout.Footer;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ReviewService.Blazor.Client.Pages.EvaluationPoints
{
    public partial class EvaluationPointsCreate
    {
        private EvaluationPointsTemplateApiModel _evaluationPointsTemplate;
        private EvaluationPointApiModel _evaluationPoint;
        private List<FooterButton> _buttons;
        private EditForm _editForm;
        private EditForm _editFormItem;

        [Inject]
        public IDialogService DialogService { get; set; }

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
            _buttons = new List<FooterButton>();
        }

        protected override void OnInitialized()
        {
            ApplicationState.SetState("Evaluation Point Add", SetButtons());
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
            if(_editFormItem.EditContext.Validate())
            {
                _evaluationPointsTemplate.EvaluationPoints.Add(_evaluationPoint);
                _evaluationPoint = new EvaluationPointApiModel();
                StateHasChanged();
            }
        }

        private async void OnDeleteClicked(EvaluationPointApiModel point)
        {
            var message = $"Actually delete \"{point.Name}\" evaluation point item?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteEvaluationPointItem(point);
            }
        }

        private void DeleteEvaluationPointItem(EvaluationPointApiModel point)
        {
            _evaluationPointsTemplate.EvaluationPoints.Remove(point);
            StateHasChanged();
        }

        private async void OnSaveClicked()
        {
            if(_editForm.EditContext.Validate())
            {
                await HttpClient.PostAsJsonAsync("api/EvaluationPoint", _evaluationPointsTemplate);
                NavigationManager.NavigateTo("/evaluationpoints");
            }
        }

        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/evaluationpoints");
        }


    }
}
