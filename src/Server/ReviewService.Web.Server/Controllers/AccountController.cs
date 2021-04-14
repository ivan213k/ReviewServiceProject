using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Users.Interfaces;
using ReviewService.Shared.AuthorizationDtos;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public AccountController(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Login(UserForAuthenticationDto userForAuthentication)
        {
            var authResult = await _identityService.AuthorizeAsync(userForAuthentication.Email, userForAuthentication.Password);
            if (authResult.IsAuthSuccessful)
            {
                return Ok(_mapper.Map<AuthResponseDto>(authResult));
            }
            return Unauthorized(_mapper.Map<AuthResponseDto>(authResult));
        }

    }
}
