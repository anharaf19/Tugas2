using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2APIProvider.Services;
using Tugas2APIProvider.ViewModels;

namespace Tugas2APIProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Registration(CreateUserViewModel createUserViewModel)
        {
            try
            {
                await _user.Registration(createUserViewModel);
                return Ok($"Registrasi user {createUserViewModel.Username} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserViewModel>> Authenticate(CreateUserViewModel createUserViewModel)
        {
            try
            {
                var user = await _user.Authenticate(createUserViewModel.Username, createUserViewModel.Password);
                if (user == null)
                    return BadRequest("Username/pass not match");
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
