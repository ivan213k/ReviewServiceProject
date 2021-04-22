using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class ReviewSessionJsonModel
    {
        public ReviewSessionJsonModel()
        {

        }

        public ReviewSessionJsonModel(List<Area> areas, List<FinalReviewEvaluation> finalReviewEvaluations = null)
        {
            Areas = areas;
            FinalReviewEvaluations = finalReviewEvaluations;
        }

        public List<Area> Areas { get; set; }
        public List<FinalReviewEvaluation> FinalReviewEvaluations { get; set; }
    }
}
