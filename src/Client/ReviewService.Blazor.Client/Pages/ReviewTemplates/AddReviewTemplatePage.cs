using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Blazor.Client.Layout;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewTemplates
{
    public partial class AddReviewTemplatePage
    {
        private ReviewTemplateApiModel reviewTemplate;
        private EditForm editForm;
        private List<EvaluationPointsTemplateApiModel> evaluationPointsTemplates;
        private List<EvaluationPointApiModel> evaluationPoints;
        private List<AreaApiModel> areas;

        [Parameter]
        public int? Id { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public AddReviewTemplatePage()
        {
            reviewTemplate = new ReviewTemplateApiModel();
            reviewTemplate.Areas = new List<AreaApiModel>();
            evaluationPoints = new List<EvaluationPointApiModel>();
        }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetHeaderTitle("Review Template Add");
            evaluationPointsTemplates = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
            if (Id != null)
            {
                //header.SetTitle("Review Template Edit");
                reviewTemplate = await HttpClient.GetFromJsonAsync<ReviewTemplateApiModel>($"api/ReviewTemplate/{Id}");
                evaluationPoints = evaluationPointsTemplates.Find(r => r.Id == reviewTemplate.EvaluationPointsTemplateId).EvaluationPoints;
            }
        }
       
        private void AddAreaRow(AreaApiModel area)
        {
            if (reviewTemplate.Areas.Any(a => a.Name == area.Name))
            {
                return;
            }
            reviewTemplate.Areas.Add(area);
            StateHasChanged();
        }
        private void DeleteAreaRow(AreaApiModel area)
        {
            reviewTemplate.Areas.Remove(area);
        }
        
        private async void OnSaveClicked()
        {
            if (editForm.EditContext.Validate())
            {
                if (Id is null)
                {
                    await HttpClient.PostAsJsonAsync("api/ReviewTemplate", reviewTemplate);
                }
                else
                {
                    await HttpClient.PutAsJsonAsync("api/ReviewTemplate", reviewTemplate);
                }
                NavigateToReviewTemplates();
            }
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/reviewTemplates");
        }
        private void OnEvaluationPointTemplateSelectionChanged(int pointTemplateId)
        {
            evaluationPoints = evaluationPointsTemplates.Find(r => r.Id == pointTemplateId).EvaluationPoints;
            reviewTemplate.EvaluationPointsTemplateId = pointTemplateId;
            reviewTemplate.MidEvaluationPointId = evaluationPoints.FirstOrDefault().Id;
        }

        private void NavigateToReviewTemplates()
        {
            NavigationManager.NavigateTo("/reviewTemplates");
        }
        private async Task<List<AreaApiModel>> LoadAreas()
        {
            return await HttpClient.GetFromJsonAsync<List<AreaApiModel>>("api/Area");
        }
        private async Task<IEnumerable<string>> SearchAreas(string value)
        {
            areas = await LoadAreas();

            if (string.IsNullOrEmpty(value))
                return areas.Select(a => a.Name).ToList();
            return areas.Where(a => a.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(a=>a.Name);
        }
        private void SearchAreaValueChanged(string value)
        {
            var selectedArea = areas.FirstOrDefault(a => a.Name == value);
            if (selectedArea is null)
            {
                return;
            }
            AddAreaRow(selectedArea);
        }
    }
}
