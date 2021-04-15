using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class FinalReviewAreaItem
    {
        public int AreaItemId { get; set; }
        public string Name { get; set; }
        public string Middle { get; set; }
        public List<Reviewer> Reviewers { get; set; }
        public string FinalReview { get; set; }
    }
}
