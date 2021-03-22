﻿using Microsoft.AspNetCore.Components;
using ReviewService.Blazor.Client.Components;
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
        private DeleteConfirmation deleteConfirmationDialog;
        private ReviewTemplateApiModel reviewTemplateForDeletion;

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
        }

        private void OnDeleteClicked(ReviewTemplateApiModel reviewTemplate)
        {
            reviewTemplateForDeletion = reviewTemplate;
            deleteConfirmationDialog.Show($"Actually delete\"{reviewTemplate.Name}\" area ?");
        }
        private async void DeleteReviewTemplate()
        {
            await HttpClient.DeleteAsync($"api/ReviewTemplate/{reviewTemplateForDeletion.Id}");
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
            StateHasChanged();
        }
    }
}
