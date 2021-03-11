using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AreaItem> AreaItems { get; set; }
        public List<ReviewTemplate> ReviewTemplates { get; set; }
    }
}
