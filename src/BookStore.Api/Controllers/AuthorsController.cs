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
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
        => _service = service;



    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthorResponseDto>> GetById([FromRoute] int id)
    {
        try
        {
            if (id <= 0) 
                return BadRequest("The id must be a number greater than zero");
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(await _service.GetByIdAsync(id));

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
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        try
        {
            var authors = await _service.GetAllAsync();

            return Ok(authors);
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
    public async Task<IActionResult> Create([FromBody] AuthorDto author)
    {
        try
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            author = await _service.CreateAsync(author);

            return CreatedAtAction(nameof(GetById), new { Id = author.Id}, author);
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
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AuthorDto author)
    {
        try
        {
            if (id <= 0) 
                return BadRequest("The id must be a number greater than zero");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(id, author);

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
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            if (id <= 0) 
                return BadRequest("The id must be a number greater than zero");

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
