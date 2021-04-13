using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class EvaluationJsonModel
    {
        public int ReviewEvaluationId { get; set; }
        public List<EvaluationArea> Areas { get; set; }
    }
}
