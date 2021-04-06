using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewService.Application.Users.Interfaces;
using ReviewService.Shared.ApiModels;
using ReviewService.Shared.ApiModels.UserViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityService _userService;
        private readonly IMapper _mapper;
        public UsersController(IIdentityService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<UserApiModel>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return _mapper.Map<List<UserApiModel>>(users);
        }

        [HttpPost]
        public async Task CreateUser(CreateUserViewModel model) 
        {
            await _userService.CreateUserAsync(model.FullName, model.Email, model.Password);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(string id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
