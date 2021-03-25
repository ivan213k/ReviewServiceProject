using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Blazor.Client.Layout;
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
        private Header header;
        private List<EvaluationPointsTemplateApiModel> evaluationPointsTemplates;
        private List<EvaluationPointApiModel> evaluationPoints;

        [Parameter]
        public int? Id { get; set; }

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
            evaluationPointsTemplates = await HttpClient.GetFromJsonAsync<List<EvaluationPointsTemplateApiModel>>("api/EvaluationPoint");
            if (Id != null)
            {
                header.SetTitle("Review Template Edit");
                reviewTemplate = await HttpClient.GetFromJsonAsync<ReviewTemplateApiModel>($"api/ReviewTemplate/{Id}");
                evaluationPoints = evaluationPointsTemplates.Find(r => r.Id == reviewTemplate.EvaluationPointsTemplateId).EvaluationPoints;
            }
        }

        private void DeleteAreaRow(AreaApiModel area)
        {
            reviewTemplate.Areas.Remove(area);
        }
        private void AddAreaRow(AreaApiModel area)
        {
            if (reviewTemplate.Areas.Any(a=>a.Name == area.Name))
            {
                return;
            }
            reviewTemplate.Areas.Add(area);
            StateHasChanged();
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/reviewTemplates");
        }
        private async void OnSaveClicked() 
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
        private void OnEvaluationPointTemplateSelectionChanged(ChangeEventArgs args)
        {
            if (int.TryParse(args.Value.ToString(), out int pointTemplateId))
            {
                evaluationPoints = evaluationPointsTemplates.Find(r => r.Id == pointTemplateId).EvaluationPoints;
                reviewTemplate.MidEvaluationPointId = evaluationPoints.FirstOrDefault().Id;
            }
        }

        private void NavigateToReviewTemplates()
        {
            NavigationManager.NavigateTo("/reviewTemplates");
        }
    }
}
