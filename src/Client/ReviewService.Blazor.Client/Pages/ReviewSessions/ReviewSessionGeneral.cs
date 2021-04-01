using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

        [Inject]
        public ApplicationState ApplicationState { get; set; }

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
                ApplicationState.SetState($"Review session - {reviewSession.Name}", CreateFooterButtons());
            }
        }

        private List<FooterButton> CreateFooterButtons()
        {
            var buttons = new List<FooterButton>
            {
                new FooterButton("Cancel", CancelClicked),
                new FooterButton("Save",SaveClicked)
            };
            return buttons;
        }

        private void CancelClicked()
        {
            NavigationManager.NavigateTo("/reviewSessions");
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
