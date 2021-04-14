using ReviewService.Shared.AuthorizationDtos;
using System.Threading.Tasks;

namespace ReviewService.Blazor.Client.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
