using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewViewTable
    {
        private List<FinalReviewAreaApiModel> finalReviewAreas;

        private FinalReviewAreaApiModel currentFinalReviewArea;
        private int currentAreaIndex = 1;

        [Parameter]
        public int SessionId { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }
        protected override async Task OnInitializedAsync()
        {
            finalReviewAreas = await HttpClient.GetFromJsonAsync<List<FinalReviewAreaApiModel>>($"api/PersonalReviewView/{SessionId}");
            currentFinalReviewArea = finalReviewAreas.FirstOrDefault();
        }

        private void NextPage()
        {
            if (currentAreaIndex < finalReviewAreas.Count)
            {
                currentAreaIndex++;
                currentFinalReviewArea = finalReviewAreas[currentAreaIndex - 1];
            }
        }
        private void PreviousPage()
        {
            if (currentAreaIndex > 1)
            {
                currentAreaIndex--;
                currentFinalReviewArea = finalReviewAreas[currentAreaIndex - 1];
            }
        }
        private void SelectedAreaChanged(HashSet<FinalReviewAreaApiModel> selectedAreas)
        {
            currentAreaIndex = finalReviewAreas.IndexOf(currentFinalReviewArea) + 1;
        }
    }
}
