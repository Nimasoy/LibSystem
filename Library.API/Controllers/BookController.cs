using Library.Application.DTOs.Book;
using Library.Application.Features.Books.Commands;
using Library.Application.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookDto createBookDto)
    {
        var command = new AddBookCommand(createBookDto);
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _mediator.Send(new GetAllBooksQuery());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id));
        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        if (id != updateBookDto.Id)
            return BadRequest("ID mismatch.");

        await _mediator.Send(new UpdateBookCommand(updateBookDto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteBookCommand(id));
        return NoContent();
    }

}
