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
public class EditionTypesController : ControllerBase
{
    private readonly IEditionTypeService _service;

    public EditionTypesController(IEditionTypeService service)
        => _service = service;


    [HttpGet("{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EditionTypeDto>> GetByCode([FromRoute] string code)
    {
        try
        {
            if (!IsValidCode(code))
                return BadRequest("The code cannot be null and must have 5, all uppercase letters");

            var editionType = await _service.GetByCodeAsync(code);

            return Ok(editionType);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EditionTypeDto>>> GetAll()
    {
        try
        {
            var editionTypes = await _service.GetAllAsync();

            return Ok(editionTypes);
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
    public async Task<IActionResult> Create([FromBody] EditionTypeDto editionType)
    {
        try
        {
            if (editionType is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            editionType = await _service.CreateAsync(editionType);

            return CreatedAtAction(nameof(GetByCode), new { code = editionType.Code }, editionType);
        }
        catch (Exception)
        {
            throw;
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{code}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] string code, [FromBody] EditionTypeDto editionType)
    {
        try
        {
            if (!IsValidCode(code))
                return BadRequest("The code cannot be null and must have 5, all uppercase letters");

            if (editionType is null)
                return BadRequest("The resource to be created cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(code, editionType);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(ex.Message);
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
    }


    private bool IsValidCode(string code)
        => Regex.IsMatch(code, @"^[A-Z]{5}$");
}
