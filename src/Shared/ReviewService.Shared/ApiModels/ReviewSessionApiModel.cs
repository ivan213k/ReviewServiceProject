using ReviewService.Shared.ApiEnums;
using System;
using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels
{
    public class ReviewSessionApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ReviewSessionStatusApiEnum Status { get; set; }
        public string ReviewMaster { get; set; }
        public string PersonUnderReview { get; set; }
        public DateTime? DueDate { get; set; }
        public string Session_json { get; set; }
        public List<ReviewEvaluationApiModel> ReviewEvaluations { get; set; }
        public int EvaluationPointsTemplateId { get; set; }
        public int MidEvaluationPointId { get; set; }
    }
}
