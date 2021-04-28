using ReviewService.Domain.Enums;

namespace ReviewService.Domain.Entites
{
    public class PersonalReview
    {
        public string Session { get; set; }
        public string PersonUnderReview { get; set; }
        public ReviewEvaluationStatus ReviewStatus { get; set; }
        public string ReviewLink { get; set; }
    }
}
