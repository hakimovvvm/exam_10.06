namespace Domain.DTOs.BookDTOs;

public class CreateBookDTO
{
    public string Title { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int AuthorId { get; set; }
}
