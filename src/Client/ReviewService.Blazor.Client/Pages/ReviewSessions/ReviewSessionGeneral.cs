using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionGeneral
    {
        private ReviewSessionApiModel reviewSession;
        private List<UserApiModel> users;
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

        [Inject]
        public IDialogService DialogService { get; set; }

        public ReviewSessionGeneral()
        {
            reviewSession = new ReviewSessionApiModel();
            reviewSession.ReviewEvaluations = new List<ReviewEvaluationApiModel>();
        }
        protected override async Task OnInitializedAsync()
        {
            if (ReviewSessionId != null)
            {
                reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{ReviewSessionId}");
                ApplicationState.SetState($"Review session - {reviewSession.Name}", CreateFooterButtons());
            }
            else
            {
                ApplicationState.SetState($"Review session create", CreateFooterButtons());
            }
        }

        private async Task<List<UserApiModel>> LoadUsers()
        {
            return await HttpClient.GetFromJsonAsync<List<UserApiModel>>("api/Users");
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
            NavigateToReviewSessions();
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
                NavigateToReviewSessions();
            }
        }

        private void NavigateToReviewSessions()
        {
            NavigationManager.NavigateTo("/reviewSessions");
        }

        private async Task<IEnumerable<string>> SearchUsers(string value)
        {
            users = await LoadUsers();

            if (string.IsNullOrEmpty(value))
                return users.Select(a => a.FullName).ToList();
            return users.Where(a => a.FullName.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(a => a.FullName);
        }
        private void SearchUserValueChanged(string value)
        {
            var selectedUser = users.FirstOrDefault(a => a.FullName == value);
            if (selectedUser is null)
            {
                return;
            }
            AddReviewerRow(selectedUser);
        }
        private void AddReviewerRow(UserApiModel user) 
        {
            if (reviewSession.ReviewEvaluations.Any(a => a.UserId == user.Id))
            {
                return;
            }
            reviewSession.ReviewEvaluations.Add(new ReviewEvaluationApiModel() 
            {
                Reviewer = user.FullName,
                UserId = user.Id,
                Guid = Guid.NewGuid()
            });
        }
        private void DeleteReviewerRow(ReviewEvaluationApiModel reviewEvaluation)
        {
            reviewSession.ReviewEvaluations.Remove(reviewEvaluation);
        }
        private async void DeleteSessionClicked()
        {
            var message = $"Actually delete\"{reviewSession.Name}\" session ?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteReviewSession(reviewSession.Id);
                NavigateToReviewSessions();
            }
        }
        private async void DeleteReviewSession(int reviewSessionId)
        {
            await HttpClient.DeleteAsync($"api/ReviewSession/{reviewSessionId}");
        }
    }
}
