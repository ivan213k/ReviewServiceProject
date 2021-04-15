using ReviewService.Shared.ApiEnums;
using System;

namespace ReviewService.Shared.ApiModels
{
    public class ReviewEvaluationApiModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public Guid Guid { get; set; }
        public string UserId { get; set; }
        public ReviewEvaluationStatusApiEnum Status { get; set; }
        public string Evaluation_json { get; set; }
        public int ReviewSessionId { get; set; }
    }
}
