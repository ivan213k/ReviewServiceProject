using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewTemplates
{
    public partial class ReviewTemplatesPage
    {
        private List<ReviewTemplateApiModel> reviewTemplates;

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            reviewTemplates = await HttpClient.GetFromJsonAsync<List<ReviewTemplateApiModel>>("api/ReviewTemplate");
        }
    }
}
