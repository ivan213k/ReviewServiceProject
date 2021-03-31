using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionGeneral
    {
        private ReviewSessionApiModel reviewSession;
        private List<ReviewEvaluationApiModel> reviewEvaluations;
        private EditForm editForm;

        [Parameter]
        public int TemplateId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ReviewSessionGeneral()
        {
            reviewSession = new ReviewSessionApiModel();
            reviewEvaluations = new List<ReviewEvaluationApiModel>();
        }
        private async void SaveClicked()
        {
            if (editForm.EditContext.Validate())
            {
                var response = await HttpClient.PostAsJsonAsync($"api/ReviewSession/{TemplateId}", reviewSession);
                NavigationManager.NavigateTo("/reviewSessions");
            }   
        }
    }
}
