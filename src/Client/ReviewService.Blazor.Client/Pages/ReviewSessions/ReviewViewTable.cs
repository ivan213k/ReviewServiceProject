using Microsoft.AspNetCore.Components;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.PersonalReviewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ReviewService.Blazor.Client.Pages.ReviewSessions
{
    public partial class ReviewViewTable
    {
        private FinalReviewAreaApiModel currentFinalReviewArea;
        private int currentAreaIndex = 1;
        
        [Parameter]
        public List<FinalReviewAreaApiModel> FinalReviewAreas { get; set; }

        [Parameter]
        public EvaluationPointsTemplateApiModel EvaluationPointsTemplate { get; set; }
        
        [Inject]
        public HttpClient HttpClient { get; set; }

        protected override void OnParametersSet()
        {
            if (FinalReviewAreas != null)
            {
                currentFinalReviewArea = FinalReviewAreas.FirstOrDefault();
            }
        }

        private void NextPage()
        {
            if (currentAreaIndex < FinalReviewAreas.Count)
            {
                currentAreaIndex++;
                currentFinalReviewArea = FinalReviewAreas[currentAreaIndex - 1];
            }
        }
        private void PreviousPage()
        {
            if (currentAreaIndex > 1)
            {
                currentAreaIndex--;
                currentFinalReviewArea = FinalReviewAreas[currentAreaIndex - 1];
            }
        }
        private void SelectedAreaChanged(HashSet<FinalReviewAreaApiModel> selectedAreas)
        {
            currentAreaIndex = FinalReviewAreas.IndexOf(currentFinalReviewArea) + 1;
        }
    }
}
