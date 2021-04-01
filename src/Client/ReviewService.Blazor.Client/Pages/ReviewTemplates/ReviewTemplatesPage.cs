using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReviewService.Blazor.Client.Components;
using ReviewService.Blazor.Client.State;
using ReviewService.Shared.ApiModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewTemplates
{
    public partial class ReviewTemplatesPage
    {
        private List<ReviewTemplateApiModel> reviewTemplates;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        [Inject]
        public IDialogService DialogService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //ApplicationState.SetHeaderTitle("Review Templates");
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
        }
        private void OnAddReviewTemlateClicked()
        {
            NavigationManager.NavigateTo("/addReviewTemplate");
        }
        private void OnEditClicked(int reviewTemplateId)
        {
            NavigationManager.NavigateTo($"/editReviewTemplate/{reviewTemplateId}");
        }
        private async void OnDeleteClicked(ReviewTemplateApiModel reviewTemplate)
        {
            var message = $"Actually delete\"{reviewTemplate.Name}\" template ?";
            var parameters = new DialogParameters();
            parameters.Add("ContentText", message);

            var dialogResult = DialogService.Show<DeleteConfirmationDialog>("Delete", parameters);
            var result = await dialogResult.Result;
            if (!result.Cancelled)
            {
                DeleteReviewTemplate(reviewTemplate);
            }
        }
        private async void DeleteReviewTemplate(ReviewTemplateApiModel reviewTemplate)
        {
            await HttpClient.DeleteAsync($"api/ReviewTemplate/{reviewTemplate.Id}");
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
            StateHasChanged();
        }
    }
}
