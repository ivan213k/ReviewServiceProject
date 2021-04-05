using ReviewService.Domain.Enums;

namespace ReviewService.Domain.Entites
{
    public class ReviewEvaluation
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string PersonalReviewLink { get; set; }
        public ReviewEvaluationStatus Status { get; set; }
        public string Evaluation_json { get; set; }
        public int ReviewSessionId { get; set; }
    }
}
