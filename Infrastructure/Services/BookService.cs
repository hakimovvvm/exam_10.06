using System.Data;
using System.Net;
using Domain.ApiResponse;
using Domain.DTOs.BookDTOs;
using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<Response<string>> AddBookAsync(CreateBookDTO create)
    {
        var book = new Book
        {
            Title = create.Title,
            Genre = create.Genre,
            AuthorId = create.AuthorId,
            PublishedDate = DateTime.Now
        };

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> UpdateBookAsync(UpdateBookDTO update)
    {
        var book = await context.Books.FindAsync(update.Id);
        if (book == null)
            return new Response<string>("book not found", HttpStatusCode.NotFound);

        book.Title = update.Title;
        book.Genre = update.Genre;
        book.AuthorId = update.AuthorId;

        context.Books.Update(book);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
            return new Response<string>("book not found", HttpStatusCode.NotFound);

        context.Books.Remove(book);
        await context.SaveChangesAsync();

        return new Response<string>("successfully", null!);
    }

    public async Task<Response<GetBookDTO>> GetBookAsync(int id)
    {
        var book = await context.Books
            .Where(b => b.Id == id)
            .Select(b => new GetBookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Genre = b.Genre,
                AuthorId = b.AuthorId,
                PublishedDate = b.PublishedDate
            }).FirstOrDefaultAsync();

        if (book == null)
            return new Response<GetBookDTO>("book not found", HttpStatusCode.NotFound);

        return new Response<GetBookDTO>("successfully", book);
    }

    public async Task<Response<List<GetBookDTO>>> GetBooksByAuthorAsync(string authorName)
    {
        var books = await context.Books
            .Include(b => b.Author)
            .Where(b => b.Author.Name.ToLower() == authorName.ToLower())
            .Select(b => new GetBookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Genre = b.Genre,
                AuthorId = b.AuthorId,
                PublishedDate = b.PublishedDate
            }).ToListAsync();

        if (books == null || books.Count == 0)
            return new Response<List<GetBookDTO>>($"books bu this author not found", HttpStatusCode.NotFound);

        return new Response<List<GetBookDTO>>($"successfully", books);
    }


    public async Task<Response<List<GetBookDTO>>> GetBooksByGenreAsync(string genre)
    {
        var books = await context.Books
            .Where(b => b.Genre.ToLower() == genre.ToLower())
            .Select(b => new GetBookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Genre = b.Genre,
                AuthorId = b.AuthorId,
                PublishedDate = b.PublishedDate
            }).ToListAsync();

        return new Response<List<GetBookDTO>>("book by this genre not found", books);
    }

    public async Task<Response<List<GetBookDTO>>> GetRecentlyPublishedBooksAsync(int years)
    {
        var date = DateTime.Now.AddYears(-years);

        var books = await context.Books
            .Where(b => b.PublishedDate >= date)
            .Select(b => new GetBookDTO
            {
                Id = b.Id,
                Title = b.Title,
                Genre = b.Genre,
                AuthorId = b.AuthorId,
                PublishedDate = b.PublishedDate
            }).ToListAsync();

        return new Response<List<GetBookDTO>>($"books not found", books);
    }
}