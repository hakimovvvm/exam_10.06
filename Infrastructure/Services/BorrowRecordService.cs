using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.BorrowRecordDTOs;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BorrowRecordService(DataContext context) : IBorrowRecordService
{
    public async Task<Response<string>> AddBorrowRecordAsync(CreateBorrowDTO create)
    {
        var borrowRecord = new BorrowRecord
        {
            MemberId = create.MemberId,
            BookId = create.BookId,
            BorrowDate = DateTime.Now,
            ReturnDate = default
        };

        await context.BorrowRecords.AddAsync(borrowRecord);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> UpdateBorrowRecordAsync(UpdateBorrowDTO update)
    {
        var record = await context.BorrowRecords.FindAsync(update.Id);
        if (record == null)
            return new Response<string>("borrow not found", HttpStatusCode.NotFound);

        record.MemberId = update.MemberId;
        record.BookId = update.BookId;
        record.ReturnDate = update.ReturnDate;

        context.BorrowRecords.Update(record);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> DeleteBorrowRecordAsync(int id)
    {
        var record = await context.BorrowRecords.FindAsync(id);
        if (record == null)
            return new Response<string>("boorow not found", HttpStatusCode.NotFound);

        context.BorrowRecords.Remove(record);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<GetBorrowDTO>> GetBorrowRecordAsync(int id)
    {
        var record = await context.BorrowRecords
            .Where(br => br.Id == id)
            .Select(br => new GetBorrowDTO
            {
                Id = br.Id,
                MemberId = br.MemberId,
                BookId = br.BookId,
                BorrowDate = br.BorrowDate,
                ReturnDate = br.ReturnDate
            })
            .FirstOrDefaultAsync();

        if (record == null)
            return new Response<GetBorrowDTO>("borrow not found", HttpStatusCode.NotFound);

        return new Response<GetBorrowDTO>("successfully", record);
    }

    public async Task<Response<List<GetBorrowDTO>>> GetOverdueBorrowsAsync()
    {
        var today = DateTime.Now;
        var overdue = await context.BorrowRecords
            .Where(br => br.ReturnDate == null && br.BorrowDate.AddDays(30) < today)
            .Select(br => new GetBorrowDTO
            {
                Id = br.Id,
                MemberId = br.MemberId,
                BookId = br.BookId,
                BorrowDate = br.BorrowDate,
                ReturnDate = br.ReturnDate
            })
            .ToListAsync();

        return new Response<List<GetBorrowDTO>>("successfully", overdue);
    }

    public async Task<Response<List<GetBorrowDTO>>> GetBorrowHistoryByMemberAsync(string memberName)
    {
        var records = await context.BorrowRecords
            .Include(br => br.Member)
            .Where(br => br.Member.Name.ToLower() == memberName.ToLower())
            .Select(br => new GetBorrowDTO
            {
                Id = br.Id,
                MemberId = br.MemberId,
                BookId = br.BookId,
                BorrowDate = br.BorrowDate,
                ReturnDate = br.ReturnDate
            })
            .ToListAsync();

        return new Response<List<GetBorrowDTO>>($"successfully", records);
    }

    public async Task<Response<List<GetBorrowDTO>>> GetBorrowHistoryByBookAsync(string bookName)
    {
        var records = await context.BorrowRecords
            .Include(br => br.Book)
            .Where(br => br.Book.Title.ToLower() == bookName.ToLower())
            .Select(br => new GetBorrowDTO
            {
                Id = br.Id,
                MemberId = br.MemberId,
                BookId = br.BookId,
                BorrowDate = br.BorrowDate,
                ReturnDate = br.ReturnDate
            })
            .ToListAsync();

        return new Response<List<GetBorrowDTO>>($"successfully", records);
    }
}
