using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(IAuthService service, SignInManager<IdentityUser> signInManager)
        {
            _service = service;
            _signInManager = signInManager;
        }


        [HttpPost("/Users/Login")]
        public async Task<ActionResult<string>> LogIn([FromBody] UserLogin user)
        {     
            try
            {
                if (!await ValidateCredential(user))
                    return Unauthorized( new { message = "Invalid Credential" });

                return Ok("Aqui proximamente se enviara JWT");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }           
        }


        [HttpPost("/Users/SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserSingUpDto user)
        {
            try
            {
                if(!ModelState.IsValid) 
                    return BadRequest(ModelState);

                await _service.SignUpAsync(user);

                string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                return Created(baseUrl, new {UserName = user.Username});
            }
            catch(AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }


        [HttpPost("/Roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {           
            try
            {
                await _service.CreateRoleAsync(roleName);

                string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

                return Created(baseUrl, new {RoleName = roleName});
            }
            catch(AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            } 
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
