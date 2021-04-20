using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.Layout.Footer;
using ReviewService.Blazor.Client.Services;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewSessionGeneral : IDisposable
    {
        private ReviewSessionApiModel reviewSession;
        private List<FinalReviewAreaApiModel> finalReviewAreas;
        private EvaluationPointsTemplateApiModel evaluationPointsTemplate;
        private List<UserApiModel> users;
        private EditForm editForm;
        private Dictionary<int, List<FooterButton>> tabsFooterButtons;
        private MudTabs mudTabs;

        [Parameter]
        public int TemplateId { get; set; }

        [Parameter]
        public int? ReviewSessionId { get; set; }

        [Parameter]
        public int TabIndex { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }
        
        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            if (ReviewSessionId is null)
            {
                reviewSession = new ReviewSessionApiModel();
                reviewSession.ReviewEvaluations = new List<ReviewEvaluationApiModel>();
            }
            else
            {
                reviewSession = await HttpClient.GetFromJsonAsync<ReviewSessionApiModel>($"api/ReviewSession/{ReviewSessionId}");
                evaluationPointsTemplate = await HttpClient.GetFromJsonAsync<EvaluationPointsTemplateApiModel>($"api/EvaluationPoint/{reviewSession.EvaluationPointsTemplateId}");
                finalReviewAreas = await HttpClient.GetFromJsonAsync<List<FinalReviewAreaApiModel>>($"api/PersonalReviewView/{ReviewSessionId}");
            }
            SetApplicationState(CreateGeneralTabFooterButtons());
            tabsFooterButtons = InitializeTabsFooterButtons();
            
        }
        protected override void OnAfterRender(bool firstRender)
        {
            if (mudTabs != null && mudTabs.ActivePanelIndex != TabIndex)
            {
                mudTabs.ActivatePanel(TabIndex);
            }   
        }
        private void SetApplicationState(List<FooterButton> footerButtons) 
        {
            string header = ReviewSessionId is null ? "Review session create" : $"Review session - {reviewSession.Name}";
            ApplicationState.SetState(header, footerButtons);
        }
        private Dictionary<int, List<FooterButton>> InitializeTabsFooterButtons() 
        {
            var tabsFooterButtons = new Dictionary<int, List<FooterButton>>
            {
                { 0, CreateGeneralTabFooterButtons() },
                { 1, CreateViewTabFooterButtons() },
                { 2, new List<FooterButton>() }
            };
            return tabsFooterButtons;
        }

        private List<FooterButton> CreateGeneralTabFooterButtons()
        {
            var buttons = new List<FooterButton>
            {
                new FooterButton("Cancel", CancelClicked),
                new FooterButton("Save",SaveClicked)
            };
            return buttons;
        }
        private List<FooterButton> CreateViewTabFooterButtons()
        {
            var buttons = new List<FooterButton>
            {
                new FooterButton("View on fullscreen", NavigateToFullScreen),
                new FooterButton("Publish", SaveFinalReviews)
            };
            return buttons;
        }
        private async Task<List<UserApiModel>> LoadUsers()
        {
            return await HttpClient.GetFromJsonAsync<List<UserApiModel>>("api/Users");
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
                    var response = await HttpClient.PostAsJsonAsync($"api/ReviewSession/{TemplateId}", reviewSession);
                    if (response.IsSuccessStatusCode)
                    {
                        NavigateToReviewSessions();
                    }
                }
                else
                {
                    await HttpClient.PutAsJsonAsync($"api/ReviewSession", reviewSession);
                    await DialogService.ShowMessageBox("Information", "Session saved successfully!");
                }
            }
        }
        private async void SaveFinalReviews() 
        {
            var result = await HttpClient.PutAsJsonAsync($"api/PersonalReviewView/{ReviewSessionId}", finalReviewAreas);
            if (result.IsSuccessStatusCode)
            {
                await DialogService.ShowMessageBox("Information", "Final reviews published successfully!");
            }
        }
        private void NavigateToFullScreen() 
        {
            NavigationManager.NavigateTo($"/reviewViewTableFullScrean/{ReviewSessionId}");
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

        private void OnActivePanelIndexChanged(int activeTabIndex) 
        {
            TabIndex = activeTabIndex;
            SetApplicationState(tabsFooterButtons[activeTabIndex]);
        }
        public void Dispose() => Interceptor.DisposeEvent();
    }
}
