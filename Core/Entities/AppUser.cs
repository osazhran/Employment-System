using Microsoft.AspNetCore.Identity;

namespace Core.Entities;
public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsEmployer { get; set; }
}