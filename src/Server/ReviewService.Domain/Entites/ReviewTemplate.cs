using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class ReviewTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Area> Areas { get; set; }
        public int EvaluationPointsTemplateId { get; set; }
        public EvaluationPoint MidEvaluationPoint { get; set; }
        public int MidEvaluationPointId { get; set; }
    }
}
