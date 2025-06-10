using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Author
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(25, ErrorMessage = "name cant be longer than 25")]
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }

    public List<Book> Books { get; set; }
}
