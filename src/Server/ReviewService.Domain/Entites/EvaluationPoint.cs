using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class EvaluationPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EvaluationPointItem> EvaluationPointItems { get; set; }
    }
}
