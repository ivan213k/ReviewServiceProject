using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class EvaluationJsonApiModel
    {
        public int ReviewEvaluationId { get; set; }
        public List<EvaluationAreaApiModel> Areas { get; set; }
    }
}
