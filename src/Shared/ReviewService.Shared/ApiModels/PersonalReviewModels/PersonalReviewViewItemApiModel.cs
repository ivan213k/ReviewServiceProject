using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class PersonalReviewViewItemApiModel
    {
        public string AreaItem { get; set; }
        public string Middle { get; set; }
        public List<ReviewerApiModel> Reviewers { get; set; }
        public string FinalReview { get; set; }
    }
}
