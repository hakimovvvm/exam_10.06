namespace Domain.DTOs.MemberDTOs;

public class GetMemberDTO : CreateMemberDTO
{
    public int Id { get; set; }
    public DateTime MembershipDate { get; set; }
}
