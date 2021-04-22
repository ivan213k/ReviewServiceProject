using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels.PersonalReviewModels
{
    public class FinalReviewAreaApiModel
    {
        public string Name { get; set; }
        public List<FinalReviewAreaItemApiModel> ViewItems { get; set; }
    }
}
