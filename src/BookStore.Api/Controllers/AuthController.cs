using BookStore.Common.Exceptions;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace BookStore.Api.Controllers;

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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> LogIn([FromBody] UserLogin user)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!await ValidateCredential(user))
                return Unauthorized(new { message = "Invalid Credential" });

            var token = await _service.GetJwtTokenAsync(user.Username);

            return Ok(new { Token = token });
        }
        catch (ResourceNotFoundException)
        {
            return Unauthorized(new { message = "Invalid Credential" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPost("/Users/SignUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUp([FromBody] UserSingUpDto user)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.SignUpAsync(user);

            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            return Created(baseUrl, new { UserName = user.Username });
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPost("/Roles")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(roleName))
                return BadRequest(new {Error = "The role name cannot be null or empty"});

            var role = await _service.CreateRoleAsync(roleName);

            return CreatedAtAction(nameof(GetRole), new {Id = role.Id}, role);
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("/Roles/{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRole([FromRoute] Guid id)
    {
        try
        {
            if(id == Guid.Empty)
                return BadRequest(new {Error = "The id parameter is mandatory"});

            var role = await _service.GetRoleByIdAsync(id);

            return Ok(role);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(new {Message = ex.Message});
        }
        catch (Exception ex)
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
