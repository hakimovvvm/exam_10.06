namespace Domain.DTOs.BookDTOs;

public class GetBookDTO : CreateBookDTO
{
    public int Id { get; set; }
    public DateTime PublishedDate { get; set; }
}
