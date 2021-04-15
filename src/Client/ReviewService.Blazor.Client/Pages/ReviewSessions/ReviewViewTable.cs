using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewViewTable
    {
        private List<PersonalReviewViewItemApiModel> viewItems { get; set; }
        
        [Parameter]
        public int SessionId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            viewItems = await HttpClient.GetFromJsonAsync<List<PersonalReviewViewItemApiModel>>($"api/PersonalReviewView/{SessionId}");
        }
    }
}
