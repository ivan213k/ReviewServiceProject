using ReviewService.Shared.ApiEnums;

namespace ReviewService.Shared.ApiModels
{
    public class ReviewEvaluationApiModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string PersonalReviewLink { get; set; }
        public ReviewEvaluationStatusApiEnum Status { get; set; }
        public string Evaluation_json { get; set; }
        public int ReviewSessionId { get; set; }
    }
}
