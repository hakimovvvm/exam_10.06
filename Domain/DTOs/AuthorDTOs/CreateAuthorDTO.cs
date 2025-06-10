namespace Domain.DTOs.AuthorDTOs;

public class CreateAuthorDTO
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}
