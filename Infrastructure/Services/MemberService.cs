using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.MemberDTOs;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class MemberService(DataContext context) : IMemberService
{
    public async Task<Response<string>> AddMemberAsync(CreateMemberDTO create)
    {
        var member = new Member
        {
            Name = create.Name,
            Email = create.Email,
            MembershipDate = DateTime.Now
        };

        await context.Members.AddAsync(member);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> UpdateMemberAsync(UpdateMemberDTO update)
    {
        var member = await context.Members.FindAsync(update.Id);
        if (member == null)
            return new Response<string>("member not found", HttpStatusCode.NotFound);

        member.Name = update.Name;
        member.Email = update.Email;

        context.Members.Update(member);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> DeleteMemberAsync(int id)
    {
        var member = await context.Members.FindAsync(id);
        if (member == null)
            return new Response<string>("member not found", HttpStatusCode.NotFound);

        context.Members.Remove(member);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<GetMemberDTO>> GetMemberAsync(int id)
    {
        var member = await context.Members
            .Where(m => m.Id == id)
            .Select(m => new GetMemberDTO
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                MembershipDate = m.MembershipDate
            })
            .FirstOrDefaultAsync();

        if (member == null)
            return new Response<GetMemberDTO>("member not found", HttpStatusCode.NotFound);

        return new Response<GetMemberDTO>("successfully", member);
    }

    public async Task<Response<List<GetMemberDTO>>> GetMembersWithRecentBorrowsAsync(int days)
    {
        var date = DateTime.Now.AddDays(-days);

        var members = await context.Members
            .Where(m => m.BorrowRecords.Any(br => br.BorrowDate >= date))
            .Select(m => new GetMemberDTO
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                MembershipDate = m.MembershipDate
            })
            .ToListAsync();

        return new Response<List<GetMemberDTO>>($"successfully", members);
    }

    public async Task<Response<List<GetMemberDTO>>> GetTopNMembersByBorrowsAsync(int n)
    {
        var members = await context.Members
            .Select(m => new
            {
                Member = m,
                BorrowCount = m.BorrowRecords.Count()
            })
            .OrderByDescending(x => x.BorrowCount)
            .Take(n)
            .Select(x => new GetMemberDTO
            {
                Id = x.Member.Id,
                Name = x.Member.Name,
                Email = x.Member.Email,
                MembershipDate = x.Member.MembershipDate
            })
            .ToListAsync();

        return new Response<List<GetMemberDTO>>($"successfully", members);
    }
}
