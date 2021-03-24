using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Shared.ApiModels;
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
        private List<AreaApiModel> selectedAreas;
        private List<EvaluationPointsTemplateApiModel> evaluationPointsTemplates;
        private List<EvaluationPointApiModel> evaluationPoints;
        
        [Inject]
        public HttpClient HttpClient { get; set; }
       
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public AddReviewTemplatePage()
        {
            reviewTemplate = new ReviewTemplateApiModel();
            selectedAreas = new List<AreaApiModel>();
            evaluationPoints = new List<EvaluationPointApiModel>();
        }

        protected override async Task OnInitializedAsync()
        {
            evaluationPointsTemplates = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
        }

        private void DeleteAreaRow(AreaApiModel area)
        {
            selectedAreas.Remove(area);
        }
        private void AddAreaRow(AreaApiModel area)
        {
            if (selectedAreas.Any(a=>a.Name == area.Name))
            {
                return;
            }
            selectedAreas.Add(area);
            StateHasChanged();
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/reviewTemplates");
        }
        private async void OnSaveClicked() 
        {
            reviewTemplate.Areas = selectedAreas;
            await HttpClient.PostAsJsonAsync("api/ReviewTemplate", reviewTemplate);
            NavigationManager.NavigateTo("/reviewTemplates");
        }
        private void OnEvaluationPointTemplateSelectionChanged(ChangeEventArgs args)
        {
            if (int.TryParse(args.Value.ToString(),out int pointTemplateId))
            {
                evaluationPoints = evaluationPointsTemplates.Find(r => r.Id == pointTemplateId).EvaluationPoints;
            }
        }
    }
}
