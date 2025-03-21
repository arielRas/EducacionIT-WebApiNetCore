using BookStore.Api.Models.Mappers;
using BookStore.Common.Exceptions;
using BookStore.Services.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Authorize(Roles = "User")]
[Route("api/[controller]")]
[ApiController]
public class EditorialsController : ControllerBase
{
    private readonly IEditorialService _service;

    public EditorialsController(IEditorialService service)
        => _service = service;


    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EditorialDto>> GetById([FromRoute] int id)
    {
        try
        {
            if (id <= 0) return BadRequest("The ide cannot be less than 1");

            var editorial = await _service.GetByIdAsync(id);

            return Ok(editorial);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EditorialDto>>> GetAll()
    {
        try
        {
            var editorials = await _service.GetAllAsync();

            return Ok(editorials);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] EditorialDto editorial)
    {
        try
        {
            if (editorial is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            editorial = await _service.CreateAsync(editorial);

            return CreatedAtAction(nameof(GetById), new { Id = editorial.Id }, editorial);
        }
        catch(Exception)
        {
            throw;
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EditorialDto editorial)
    {
        try
        {
            if (id <= 0) return BadRequest("The ide cannot be less than 1");

            if (editorial is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(id, editorial);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            if (id <= 0) return BadRequest("The ide cannot be less than 1");

            await _service.DeleteAsync(id);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }
}
