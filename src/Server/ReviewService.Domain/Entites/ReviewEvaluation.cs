using ReviewService.Domain.Enums;
using System;

namespace ReviewService.Domain.Entites
{
    public class ReviewEvaluation
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public Guid Guid { get; set; }
        public string UserId { get; set; }
        public ReviewEvaluationStatus Status { get; set; }
        public string Evaluation_json { get; set; }
        public int ReviewSessionId { get; set; }
    }
}
