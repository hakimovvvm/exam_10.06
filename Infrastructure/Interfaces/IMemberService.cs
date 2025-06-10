using Domain.ApiResponse;
using Domain.DTOs.MemberDTOs;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<Response<string>> AddMemberAsync(CreateMemberDTO create);
    Task<Response<string>> UpdateMemberAsync(UpdateMemberDTO update);
    Task<Response<string>> DeleteMemberAsync(int id);
    Task<Response<GetMemberDTO>> GetMemberAsync(int id);
    Task<Response<List<GetMemberDTO>>> GetMembersWithRecentBorrowsAsync(int days);
    Task<Response<List<GetMemberDTO>>> GetTopNMembersByBorrowsAsync(int n);
}
