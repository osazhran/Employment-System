namespace Core.Dtos;
public class VacancyRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Requirements { get; set; } = null!;
    public string Responsibilities { get; set; } = null!;
    public int MaxApplications { get; set; }
    public string WorkType { get; set; } = null!;
    public string EmploymentType { get; set; } = null!;
    public int ExperienceYears { get; set; }
    public string PositionLevel { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string CompanyAddress { get; set; } = null!;
    public DateTime ExpiredAt { get; set; }
}