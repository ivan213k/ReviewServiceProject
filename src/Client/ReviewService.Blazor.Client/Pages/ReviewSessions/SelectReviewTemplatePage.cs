using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Layout.Footer;
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

        [Inject]
        public ISnackbar Snackbar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetState("Review Sessions Create", CreateFooterButtons());
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
        }

        private List<FooterButton> CreateFooterButtons()
        {
            var buttons = new List<FooterButton>
            {
                new FooterButton("Cancel", OnCancelClicked),
                new FooterButton("Select", OnSelectClicked)
            };
            return buttons;
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
                if (selectedReviewTemplate.Areas is null || selectedReviewTemplate.Areas.Count == 0)
                {
                    Snackbar.Add($"{selectedReviewTemplate.Name} template has no areas", Severity.Error);
                    return;
                }
                NavigationManager.NavigateTo($"/reviewSessionGeneral/{selectedReviewTemplate.Id}");
            }
        }
        private void OnCancelClicked()
        {
            NavigationManager.NavigateTo("/reviewSessions");
        }
    }
}
