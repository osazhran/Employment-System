namespace Core.Dtos;
public class ApplicationRequest
{
    public int VacancyId { get; set; }
    public string ResumeUrl { get; set; } = null!;
}