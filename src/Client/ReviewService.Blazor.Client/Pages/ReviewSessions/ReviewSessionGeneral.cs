using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionGeneral
    {
        private ReviewSessionApiModel reviewSession;
        private List<ReviewEvaluationApiModel> reviewEvaluations;
        private EditForm editForm;

        [Parameter]
        public int TemplateId { get; set; }

        [Parameter]
        public int? ReviewSessionId { get; set; }
       
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ReviewSessionGeneral()
        {
            reviewSession = new ReviewSessionApiModel();
            reviewEvaluations = new List<ReviewEvaluationApiModel>();
        }
        protected override async Task OnInitializedAsync()
        {
            if (ReviewSessionId != null)
            {
                reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{ReviewSessionId}");
            }
        }
        private async void SaveClicked()
        {
            if (editForm.EditContext.Validate())
            {
                if (ReviewSessionId is null)
                {
                    await HttpClient.PostAsJsonAsync($"api/ReviewSession/{TemplateId}", reviewSession);
                }
                else
                {
                    await HttpClient.PutAsJsonAsync($"api/ReviewSession", reviewSession);
                }
                NavigationManager.NavigateTo("/reviewSessions");
            }   
        }
    }
}
