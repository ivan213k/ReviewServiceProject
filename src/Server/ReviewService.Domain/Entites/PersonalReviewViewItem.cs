using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class PersonalReviewViewItem
    {
        public string AreaItem { get; set; }
        public string Middle { get; set; }
        public List<Reviewer> Reviewers { get; set; }
        public string FinalReview { get; set; }
    }
}
