using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class SelectReviewTemplatePage
    {
        private List<ReviewTemplateApiModel> reviewTemplates;
        private string searchTerm;
        private ReviewTemplateApiModel selectedReviewTemplate;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetHeaderTitle("Review Sessions Create");
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
        }
        private bool FilterFunc(ReviewTemplateApiModel reviewTemplate)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return true;
            if (reviewTemplate.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        private string GetSelectedRowStyle(ReviewTemplateApiModel reviewTemplate, int index)
        {
            if (reviewTemplate == selectedReviewTemplate)
            {
                return "background: #4491e0";
            }
            return "";
        }
        private void OnSelectClicked()
        {
            if (selectedReviewTemplate != null)
            {
                NavigationManager.NavigateTo($"/reviewSessionGeneral/{selectedReviewTemplate.Id}");
            }
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/reviewSessions");
        }
    }
}
