using System.Xml.Schema;
using Domain.ApiResponse;
using Domain.DTOs.BookDTOs;
using Domain.Entites;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<Response<string>> AddBookAsync(CreateBookDTO create);
    Task<Response<string>> UpdateBookAsync(UpdateBookDTO update);
    Task<Response<string>> DeleteBookAsync(int id);
    Task<Response<GetBookDTO>> GetBookAsync(int id);
    Task<Response<List<GetBookDTO>>> GetBooksByAuthorAsync(string authorName);
    Task<Response<List<GetBookDTO>>> GetBooksByGenreAsync(string genre);
    Task<Response<List<GetBookDTO>>> GetRecentlyPublishedBooksAsync(int years);
}
