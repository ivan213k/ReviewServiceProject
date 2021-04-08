using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class EvaluationArea
    {
        public string Name { get; set; }
        public List<EvaluationAreaItem> AreaItems { get; set; }
    }
}