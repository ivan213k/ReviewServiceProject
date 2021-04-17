using ReviewService.Domain.Enums;
using System;
using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class ReviewSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ReviewSessionStatus Status { get; set; }
        public string ReviewManager { get; set; }
        public string PersonUnderReview { get; set; }
        public DateTime DueDate { get; set; }
        public string Session_json { get; set; }
        public List<ReviewEvaluation> ReviewEvaluations { get; set; }

        public int EvaluationPointsTemplateId { get; set; }
        public int MidEvaluationPointId { get; set; }
    }
}
