using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class EvaluationArea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EvaluationAreaItem> AreaItems { get; set; }
    }
}