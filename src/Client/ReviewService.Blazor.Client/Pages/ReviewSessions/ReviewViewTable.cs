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
            //viewItems = new List<PersonalReviewViewItemApiModel>()
            //{
            // new PersonalReviewViewItemApiModel()
            //    {
            //     AreaItem = "ef core test",
            //     Middle = "normal",
            //     Reviewers = new List<ReviewerApiModel>
            //     {
            //         new ReviewerApiModel() {Name = "Rev1",Point = "bad",Comment ="comment" },
            //         new ReviewerApiModel() {Name = "Rev2",Point = "perfect",Comment ="comment p" }
            //     }
            //    },
            // new PersonalReviewViewItemApiModel()
            //    {
            //     AreaItem = "OOP",
            //     Middle = "normal",
            //     Reviewers = new List<ReviewerApiModel>
            //     {
            //         new ReviewerApiModel() {Name = "Rev1",Point = "perfect",Comment ="comment" },
            //         new ReviewerApiModel() {Name = "Rev2",Point = "normal",Comment ="comment p" }
            //     }
            //    },
            //};
        }
    }
}
