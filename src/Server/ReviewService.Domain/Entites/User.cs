using System.Collections.Generic;

namespace ReviewService.Domain.Entites
{
    public class User
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
