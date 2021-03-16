using System.Collections.Generic;

namespace ReviewService.Shared.ApiModels
{
    public class AreaApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AreaItemApiModel> AreaItems { get; set; }
    }
}
