using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class FinalReviewArea
    {
        public string Name { get; set; }
        public List<FinalReviewAreaItem> ViewItems { get; set; }
    }
}
