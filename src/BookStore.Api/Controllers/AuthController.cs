using BookStore.Api.Models.Mappers;
using BookStore.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("/Login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserLogin user)
        {            

            if (!await ValidateCredential(user))
                return Unauthorized( new { message = "Invalid Credential" });

            return Ok("Aqui proximamente se enviara JWT");
        }


        [HttpPost("/SignUp")]
        public async Task<IActionResult> SignUp(UserSignUp user)
        {
            var result = await _userManager.CreateAsync(user.ToIdentity(), user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        private async Task<bool> ValidateCredential(UserLogin user)
        {
            var result = await _signInManager.PasswordSignInAsync(
                                           user.Username,
                                           user.Password,
                                           isPersistent: false,
                                           lockoutOnFailure: false);

            return result.Succeeded;

        }
    }
}
