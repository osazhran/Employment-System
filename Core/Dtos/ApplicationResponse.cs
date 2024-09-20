namespace Core.Dtos;
public class ApplicationResponse
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AppliedAt { get; set; } = null!;
    public string ResumeUrl { get; set; } = null!;
    public string Status { get; set; } = null!;
}