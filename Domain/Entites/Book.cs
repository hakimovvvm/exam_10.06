using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Book
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50, ErrorMessage = "title cant be longer than 50")]
    public string Title { get; set; } = null!;
    [Required]
    [MaxLength(50, ErrorMessage = "name cant be longer than 50")]
    public string Genre { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
    public int AuthorId { get; set; }

    public Author? Author { get; set; }
    public List<BorrowRecord>? BorrowRecords { get; set; }
}
