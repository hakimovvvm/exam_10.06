using Domain.ApiResponse;
using Domain.DTOs.AuthorDTOs;

namespace Infrastructure.Interfaces;

public interface IAuthorService
{
    Task<Response<string>> AddAuthorAsync(CreateAuthorDTO create);
    Task<Response<string>> UpdateAuthorAsync(UpdateAuthorDTO update);
    Task<Response<string>> DeleteAuthorAsync(int id);
    Task<Response<GetAuthorDTO>> GetAuthorAsync(int id);
    Task<Response<List<GetAuthorDTO>>> GetAuthorsWithMostBooksAsync(int n);

}
