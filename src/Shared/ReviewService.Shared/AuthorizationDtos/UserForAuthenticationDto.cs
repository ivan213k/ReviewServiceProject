using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewService.Shared.AuthorizationDtos
{
    public class UserForAuthenticationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
