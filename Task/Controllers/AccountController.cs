using API.Extensions;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AccountController(IAccountService _accountService) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUserResponse>> Register(RegisterRequest model)
    {
        var result = await _accountService.RegisterAsync(model);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUserResponse>> Login(LoginRequest model)
    {
        var result = await _accountService.Login(model);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }
}