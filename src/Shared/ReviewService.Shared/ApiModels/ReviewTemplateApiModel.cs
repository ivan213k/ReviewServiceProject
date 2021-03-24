using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels
{
    public class ReviewTemplateApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AreaApiModel> Areas { get; set; }
        public int EvaluationPointsTemplateId { get; set; }
        public int MidEvaluationPointId { get; set; }
    }
}
