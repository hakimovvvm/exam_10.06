using Domain.ApiResponse;
using Domain.DTOs.BookDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService service)
{
    [HttpPost("AddBook")]
    public async Task<Response<string>> AddBookAsync(CreateBookDTO create)
    {
        return await service.AddBookAsync(create);
    }
    [HttpPut("UpdateBook")]
    public async Task<Response<string>> UpdateBookAsync(UpdateBookDTO update)
    {
        return await service.UpdateBookAsync(update);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        return await service.DeleteBookAsync(id);
    }
    [HttpGet("id:int")]
    public async Task<Response<GetBookDTO>> GetBookAsync(int id)
    {
        return await service.GetBookAsync(id);
    }
    [HttpGet("GetBooksByAuthor")]
    public async Task<Response<List<GetBookDTO>>> GetBooksByAuthorAsync(string authorName)
    {
        return await service.GetBooksByAuthorAsync(authorName);
    }
    [HttpGet("GetBooksByGenre")]
    public async Task<Response<List<GetBookDTO>>> GetBooksByGenreAsync(string genre)
    {
        return await service.GetBooksByGenreAsync(genre);
    }
    [HttpGet("GetRecentlyPublishedBooks{years:int}")]
    public async Task<Response<List<GetBookDTO>>> GetRecentlyPublishedBooksAsync(int years)
    {
        return await service.GetRecentlyPublishedBooksAsync(years);
    }

}
