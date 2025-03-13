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
public class EditionsController : ControllerBase
{
    private readonly IEditionService _service;

    public EditionsController(IEditionService service)
        => _service = service;


    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<EditionResponseDto>> GetById([FromRoute] Guid id)
    {
        try
        {   
            if(id == Guid.Empty) 
                return BadRequest("The id cannot be empty");

            var edition = await _service.GetByIdAsync(id);

            return Ok(edition);
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
    public async Task<ActionResult<IEnumerable<EditionDto>>> GetAll()
    {
        try
        {   
            var editions = await _service.GetAllAsync();

            return Ok(editions);
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [HttpGet("{bookTitle}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<EditionDto>>> GetByBookTitle(string bookTitle)
    {
        try
        {   
            if(string.IsNullOrWhiteSpace(bookTitle))
                return BadRequest("The bookTitle field cannot be empty");

            var editions = await _service.GetByBookTitle(bookTitle);

            return Ok(editions);
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
    public async Task<ActionResult<EditionDto>> Create([FromBody] EditionRequestCreateDto edition)
    {
        try
        {   
            var newEdition = await _service.CreateAsync(edition);

            return CreatedAtAction(nameof(GetById), new { Id = newEdition.Id }, newEdition);
        }
        catch (BusinessException ex)
        {
            return BadRequest(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status400BadRequest)
            );
        }
        catch (DataBaseException ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            return StatusCode(
                statusCode, ex.ToApiError(HttpContext.Request.Path.Value!, statusCode)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EditionRequestUpdateDto edition)
    {
        try
        {   
            if(id == Guid.Empty) 
                return BadRequest("The id cannot be empty");

            await _service.UpdateAsync(id, edition);

            return NoContent();
        }
        catch(BusinessException ex)
        {
            return BadRequest(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status400BadRequest)
            );
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
        catch (DataBaseException ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            return StatusCode(
                statusCode, ex.ToApiError(HttpContext.Request.Path.Value!, statusCode)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:Guid}/Isbn/Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEditionIsbn([FromRoute] Guid id, [FromBody] string isbnCode)
    {
        try
        {   
            if(id == Guid.Empty) 
                return BadRequest("The id cannot be empty");

            if(string.IsNullOrWhiteSpace(isbnCode) || isbnCode.Length != 13)
                return BadRequest("The ISBN field cannot be empty and must contain 13 characters.");

            await _service.UpdateIsbnAsync(id, isbnCode);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
        catch (DataBaseException ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            return StatusCode(
                statusCode, ex.ToApiError(HttpContext.Request.Path.Value!, statusCode)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:Guid}/Price/Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateEditionPrice([FromRoute] Guid id, [FromBody] decimal price)
    {
        try
        {   
            if(id == Guid.Empty) 
                return BadRequest("The id cannot be empty");

            if(price < 0)
                return BadRequest("The price field cannot be less than zero");

            await _service.UpdateEditionPriceAsync(id, price);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
        catch (DataBaseException ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            return StatusCode(
                statusCode, ex.ToApiError(HttpContext.Request.Path.Value!, statusCode)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {   
            if(id == Guid.Empty) 
                return BadRequest("The id cannot be empty");

            await _service.DeleteAsync(id);

            return NoContent();
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
        catch (DataBaseException ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;

            return StatusCode(
                statusCode, ex.ToApiError(HttpContext.Request.Path.Value!, statusCode)
            );
        }
    }
}
