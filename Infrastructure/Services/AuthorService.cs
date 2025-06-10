using System.ComponentModel;
using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.AuthorDTOs;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthorService(DataContext context) : IAuthorService
{
    public async Task<Response<string>> AddAuthorAsync(CreateAuthorDTO create)
    {
        if (create.BirthDate > DateTime.Now)
            return new Response<string>("birthdate cant be big than now", HttpStatusCode.BadRequest);

        var author = new Author
        {
            Name = create.Name,
            BirthDate = create.BirthDate
        };

        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> UpdateAuthorAsync(UpdateAuthorDTO update)
    {
        var author = await context.Authors.FindAsync(update.Id);
        if (author == null)
            return new Response<string>("author not found", HttpStatusCode.NotFound);

        if (update.BirthDate > DateTime.Now)
            return new Response<string>("birthdate cant be big than now", HttpStatusCode.BadRequest);

        author.Name = update.Name;
        author.BirthDate = update.BirthDate;

        context.Authors.Update(author);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> DeleteAuthorAsync(int id)
    {
        var author = await context.Authors.FindAsync(id);
        if (author == null)
            return new Response<string>("author not found", HttpStatusCode.NotFound);

        context.Authors.Remove(author);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<GetAuthorDTO>> GetAuthorAsync(int id)
    {
        var author = await context.Authors
            .Where(a => a.Id == id)
            .Select(a => new GetAuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                BirthDate = a.BirthDate
            }).FirstOrDefaultAsync();

        if (author == null)
            return new Response<GetAuthorDTO>("author not found", HttpStatusCode.NotFound);

        return new Response<GetAuthorDTO>("successfully", author);
    }

    public async Task<Response<List<GetAuthorDTO>>> GetAuthorsWithMostBooksAsync(int n)
    {
        var authors = await context.Authors
            .Select(a => new
            {
                Author = a,
                BookCount = a.Books.Count()
            })
            .OrderByDescending(x => x.BookCount)
            .Take(n)
            .Select(x => new GetAuthorDTO
            {
                Id = x.Author.Id,
                Name = x.Author.Name,
                BirthDate = x.Author.BirthDate
            }).ToListAsync();

        return new Response<List<GetAuthorDTO>>($"successfully", authors);
    }
}
