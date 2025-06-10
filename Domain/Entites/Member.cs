using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Member
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(25, ErrorMessage = "name cant be longer than 25")]
    public string Name { get; set; } = null!;
    [Required]
    [MaxLength(50, ErrorMessage = "email cant be longer than 50")]
    public string Email { get; set; } = null!;
    public DateTime MembershipDate { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; }
}
