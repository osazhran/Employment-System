using Core.Dtos;
using Core.Entities;
using Core.ErrorHandling;
using System.Security.Claims;
namespace Core.Interfaces;
public interface IAccountService
{
    Task<Result<AppUserResponse>> RegisterAsync(RegisterRequest model);
    Task<Result<AppUserResponse>> Login(LoginRequest model);
    Task<AppUser> GetCurrentUser(ClaimsPrincipal User);
}