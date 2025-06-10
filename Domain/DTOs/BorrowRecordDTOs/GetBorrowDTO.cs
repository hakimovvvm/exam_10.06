namespace Domain.DTOs.BorrowRecordDTOs;

public class GetBorrowDTO : CreateBorrowDTO
{
    public int Id { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
