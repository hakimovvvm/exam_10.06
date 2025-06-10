using Domain.ApiResponse;
using Domain.DTOs.BorrowRecordDTOs;

namespace Infrastructure.Interfaces;

public interface IBorrowRecordService
{
    Task<Response<string>> AddBorrowRecordAsync(CreateBorrowDTO create);
    Task<Response<string>> UpdateBorrowRecordAsync(UpdateBorrowDTO update);
    Task<Response<string>> DeleteBorrowRecordAsync(int id);

    Task<Response<GetBorrowDTO>> GetBorrowRecordAsync(int id);
    Task<Response<List<GetBorrowDTO>>> GetOverdueBorrowsAsync();
    Task<Response<List<GetBorrowDTO>>> GetBorrowHistoryByMemberAsync(string memberName);
    Task<Response<List<GetBorrowDTO>>> GetBorrowHistoryByBookAsync(string bookName);
}
