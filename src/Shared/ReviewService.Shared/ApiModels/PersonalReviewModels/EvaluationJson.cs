using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class EvaluationJson
    {
        public string MidEvaluationPoint { get; set; }
        public List<EvaluationArea> Areas { get; set; }
    }
}
