namespace Library.Application.Features.Users.Queries.GetUserBorrowedBooks;

public class BorrowedBookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsOverdue => DateTime.Now > DueDate;
} 