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
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
        => _service = service;

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookResponseDto>> GetById([FromRoute] int id)
    {
        try
        {
            if(id <= 0)
                return BadRequest("The id must be a number greater than zero");
        
            var book = await _service.GetByIdAsync(id);
        
            return Ok(book);
        }
        catch(ResourceNotFoundException ex)
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
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAll()
    {
        try
        {      
            var books = await _service.GetAllAsync();
        
            return Ok(books);
        }
        catch(ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [HttpGet("{filter}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllFiltered([FromRoute] string filter)
    {
        try
        { 
            if(string.IsNullOrWhiteSpace(filter))     
                return BadRequest("The search filter cannot be null or contain only spaces.");

            var books = await _service.GetAllFilteredAsync(filter);
        
            return Ok(books);
        }
        catch(ResourceNotFoundException ex)
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
    public async Task<IActionResult> Create([FromBody] BookRequestDto book)
    {
        try
        { 
            if(book is null)     
                return BadRequest("The resource to be created cannot be null");

            if(!ModelState.IsValid)     
                return BadRequest(ModelState);

            var newBook = await _service.CreateAsync(book);

            return CreatedAtAction(nameof(GetById), new {Id = book.Id}, newBook);
            
        }
        catch (ResourceNotFoundException ex)
        {
            return NotFound(
                ex.ToApiError(HttpContext.Request.Path.Value!, StatusCodes.Status404NotFound)
            );
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BookDto book)
    {
        try
        {
            if(id <= 0)
                return BadRequest("The id must be a number greater than zero");
        
            await _service.UpdateAsync(id, book);
        
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
    [HttpPatch("{id:int}/Genres/Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBookGenres([FromRoute] int id, [FromBody] List<string> codes)
    {
        try
        {
            if(id <= 0)
                return BadRequest("The id must be a number greater than zero");

            if(!codes.Any())
                return BadRequest("The code list cannot be empty");

            if(codes.Count != codes.Distinct().ToList().Count)
                return BadRequest("The code list cannot have repeated elements");
            
            await _service.UpdateGenresAsync(id, codes);           
        
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
    [HttpPatch("{id:int}/Authors/Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBookAuthors([FromRoute] int id, [FromBody] List<int> authorsids)
    {
        try
        {
            if(id <= 0)
                return BadRequest("The id must be a number greater than zero");

            if(!authorsids.Any())
                return BadRequest("The code list cannot be empty");

            if(authorsids.Count != authorsids.Distinct().ToList().Count)
                return BadRequest("The code list cannot have repeated elements");
            
            if(authorsids.Any(id => id <= 0))
                return BadRequest("None of the Id's can be zero");
            
            await _service.UpdateAuthorsAsync(id, authorsids);           
        
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
            if(id <= 0)
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
    }
}
