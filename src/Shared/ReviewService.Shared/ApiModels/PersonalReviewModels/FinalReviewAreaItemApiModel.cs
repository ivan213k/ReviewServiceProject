using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class FinalReviewAreaItemApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Middle { get; set; }
        public List<ReviewerApiModel> Reviewers { get; set; }
        public string FinalReview { get; set; }
    }
}
