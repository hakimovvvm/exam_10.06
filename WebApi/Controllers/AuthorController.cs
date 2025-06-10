using Domain.ApiResponse;
using Domain.DTOs.AuthorDTOs;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api[controller]")]
public class AuthorController(IAuthorService service)
{
    [HttpPost("AddAuthor")]
    public async Task<Response<string>> AddAuthorAsync(CreateAuthorDTO create)
    {
        return await service.AddAuthorAsync(create);
    }
    [HttpPut("UpdateAuthor")]
    public async Task<Response<string>> UpdateAuthorAsync(UpdateAuthorDTO update)
    {
        return await service.UpdateAuthorAsync(update);
    }
    [HttpDelete("{id:int}")]
    public async Task<Response<string>> DeleteAuthorAsync(int id)
    {
        return await service.DeleteAuthorAsync(id);
    }
    [HttpGet("{id:int}")]
    public async Task<Response<GetAuthorDTO>> GetAuthorAsync(int id)
    {
        return await service.GetAuthorAsync(id);
    }
    [HttpGet("GetAuthorsWithMostBooks{n:int}")]
    public async Task<Response<List<GetAuthorDTO>>> GetAuthorsWithMostBooksAsync(int n)
    {
        return await service.GetAuthorsWithMostBooksAsync(n);
    }

}
