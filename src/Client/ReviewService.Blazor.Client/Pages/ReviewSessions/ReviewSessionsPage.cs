using Microsoft.AspNetCore.Components;
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
    public partial class ReviewSessionsPage
    {
        private List<ReviewSessionApiModel> reviewSessions;

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ApplicationState.SetHeaderTitle("Review Sessions");
            reviewSessions = await HttpClient.GetFromJsonAsync<List<ReviewSessionApiModel>>("api/ReviewSession");
        }
    }
}
