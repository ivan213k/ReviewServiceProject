using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class EvaluationAreaApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EvaluationAreaItemApiModel> AreaItems { get; set; }
    }
}