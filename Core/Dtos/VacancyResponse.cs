namespace Core.Dtos;
public class VacancyResponse
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Requirements { get; set; } = null!;
    public string Responsibilities { get; set; } = null!;
    public int ApplicationCount { get; set; }
    public int MaxApplications { get; set; }
    public string WorkType { get; set; } = null!;
    public string EmploymentType { get; set; } = null!;
    public int ExperienceYears { get; set; }
    public string PositionLevel { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string CompanyAddress { get; set; } = null!;
    public bool IsActive { get; set; }
    public string ExpiredAt { get; set; } = null!;
}