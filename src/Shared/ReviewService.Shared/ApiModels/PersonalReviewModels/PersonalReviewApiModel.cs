using ReviewService.Shared.ApiEnums;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class PersonalReviewApiModel
    {
        public string Session { get; set; }
        public string PersonUnderReview { get; set; }
        public ReviewEvaluationStatusApiEnum ReviewStatus { get; set; }
        public string ReviewLink { get; set; }
    }
}
