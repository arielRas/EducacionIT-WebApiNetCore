using System.Text.RegularExpressions;
using BookStore.Common.Exceptions;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Authorize(Roles = "User")]
[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _service;

    public GenresController(IGenreService service)
        => _service = service;

    [HttpGet("{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GenreDto>> GetByCode([FromRoute] string code)
    {
        try
        {
            if (!IsValidCode(code))
                return BadRequest("The code cannot be null and must have 5, all uppercase letters");

            var genre = await _service.GetByCodeAsync(code);

            return Ok(genre);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
    {
        try
        {
            var genres = await _service.GetAllAsync();

            return Ok(genres);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] GenreDto genre)
    {
        try
        {
            if (genre is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            genre = await _service.CreateAsync(genre);

            return CreatedAtAction(nameof(GetByCode), new { code = genre.Code }, genre);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{code}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] string code, [FromBody] GenreDto genre)
    {
        try
        {
            if (!Regex.IsMatch(code, @"^[A-Z]{5}$") || string.IsNullOrWhiteSpace(code))
                return BadRequest("The code cannot be null and must have 5, all uppercase letters");

            if (genre is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(code, genre);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{code}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] string code)
    {
        try
        {
            if (!IsValidCode(code))
                return BadRequest("The code cannot be null and must have 5, all uppercase letters");

            await _service.DeleteAsync(code);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    private bool IsValidCode(string code)
        => Regex.IsMatch(code, @"^[A-Z]{5}$");
}
