using Library.Application.Features.Users.Queries.GetUserBorrowedBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("me/borrowed-books")]
    public async Task<IActionResult> GetMyBorrowedBooks()
    {
        var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
        var query = new GetUserBorrowedBooksQuery(userId);
        var result = await _mediator.Send(query);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}