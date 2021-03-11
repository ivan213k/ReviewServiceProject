using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class EvaluationPointsTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ReviewTemplate> ReviewTemplates { get; set; }
        public List<EvaluationPoint> EvaluationPoints { get; set; }
    }
}
