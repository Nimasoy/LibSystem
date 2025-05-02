using Library.Application.Common;
using Library.Application.DTOs.Book;
using Library.Application.Features.Books.Commands;
using Library.Application.Features.Books.Commands.BorrowBook;
using Library.Application.Features.Books.Commands.ReturnBook;
using Library.Application.Features.Books.Queries;
using Library.Application.Features.Books.Queries.GetBookDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IMediator mediator, ILogger<BooksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    // Changed return type to include the list of BookDto
    public async Task<ActionResult<BaseResponse<IEnumerable<BookDto>>>> GetAll()
    {
        var query = new GetAllBooksQuery();
        var result = await _mediator.Send(query);
        // Returned generic BaseResponse with Data property
        return Ok(new BaseResponse<IEnumerable<BookDto>> { Success = true, Data = result });
    }

    [HttpGet("{id}")]
    [Authorize]
    // Changed return type to include a single BookDto
    public async Task<ActionResult<BaseResponse<BookDto>>> GetById(int id)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.Send(query);
         // Returned generic BaseResponse with Data property
        return Ok(new BaseResponse<BookDto> { Success = true, Data = result });
    }

    [HttpGet("{id}/details")]
    [Authorize]
    public async Task<IActionResult> GetBookDetails(int id)
    {
        var query = new GetBookDetailsQuery(id);
        var result = await _mediator.Send(query);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    // Changed return type to include the ID of the created book
    public async Task<ActionResult<BaseResponse<int>>> Add([FromBody] CreateBookDto createBookDto)
    {
        var command = new AddBookCommand(createBookDto);
        // Assuming AddBookCommand returns BaseResponse<int> based on previous fixes
        var result = await _mediator.Send(command);
        // Returned generic BaseResponse with Data property (the new book's ID)
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto updateBookDto)
    {
        if (id != updateBookDto.Id)
            return BadRequest(new BaseResponse { Success = false, Message = "ID mismatch" });

        var command = new UpdateBookCommand(updateBookDto);
        await _mediator.Send(command);
        return Ok(new BaseResponse { Success = true });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBookCommand(id);
        await _mediator.Send(command);
        return Ok(new BaseResponse { Success = true });
    }

    [HttpPost("{id}/borrow")]
    [Authorize]
    public async Task<IActionResult> BorrowBook(int id)
    {
        var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
        var command = new BorrowBookCommand(id, userId);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("{id}/return")]
    [Authorize]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
        var command = new ReturnBookCommand(id, userId);
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}