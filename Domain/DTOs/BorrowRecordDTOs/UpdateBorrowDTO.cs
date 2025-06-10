namespace Domain.DTOs.BorrowRecordDTOs;

public class UpdateBorrowDTO : CreateBorrowDTO
{
    public int Id { get; set; }
    public DateTime ReturnDate { get; set; }
}
