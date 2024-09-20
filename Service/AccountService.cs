using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.ErrorHandling;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.ConfigurationData;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Service;
public class AccountService(IMapper _mapper, RoleManager<IdentityRole> _roleManager, IOptions<JWTData> jWTData,
UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : IAccountService
{
    private readonly JWTData _jWTData = jWTData.Value;
    public async Task<Result<AppUserResponse>> RegisterAsync(RegisterRequest model)
    {
        var user = _mapper.Map<RegisterRequest, AppUser>(model);

        user.UserName = model.Email.Split('@')[0];

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded )
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure<AppUserResponse>(400, errors);
        }

        if (user.IsEmployer)//user is employer = true
        {
            var isRoleExist = await _roleManager.FindByNameAsync("Employer");

            if (isRoleExist is null)//if role  not found  create this role
                await _roleManager.CreateAsync(new IdentityRole("Employer"));

            await _userManager.AddToRoleAsync(user, "Employer"); //if this role exist add this to this user
        }
        else
        {
            var isRoleExist = await _roleManager.FindByNameAsync("Applicant");

            if (isRoleExist is null)//if role  not found  create this role
                await _roleManager.CreateAsync(new IdentityRole("Applicant"));

            await _userManager.AddToRoleAsync(user, "Applicant");//if this role exist add this to this user
        }

        var token = await GenerateAccessTokenAsync(user);//Genrate Token so i need to create(GenerateAccessTokenAsync) method 

        var userResponse = new AppUserResponse //Respons for Fornt-end After Succesful Register
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Role = user.IsEmployer ? "Employer" : "Applicant",
            Token = token
        };

        return Result.Success(userResponse);
    }
    public async Task<Result<AppUserResponse>> Login(LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);//Search For Enterd Email From Req in DB 

        if (user is null)
            return Result.Failure<AppUserResponse>(400, "The email or password you entered is incorrect, Check your credentials and try again.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded )
        {
            var errors = string.Join(", ", "The email or password you entered is incorrect, Check your credentials and try again.");
            return Result.Failure<AppUserResponse>(400, errors);
        }

        var token = await GenerateAccessTokenAsync(user);

        var userResponse = new AppUserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Token = token
        };

        return Result.Success(userResponse);
    }
    public async Task<AppUser> GetCurrentUser(ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email!);

        return user!;
    }
    private async Task<string> GenerateAccessTokenAsync(AppUser user)
    {

        var authClaims = new List<Claim>()
        {
            new (ClaimTypes.GivenName, user.UserName!),
            new (ClaimTypes.Email, user.Email!)
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTData.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _jWTData.ValidIssuer,
            audience: _jWTData.ValidAudience,
            expires: DateTime.UtcNow.AddDays(_jWTData.DurationInDays),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
   


}