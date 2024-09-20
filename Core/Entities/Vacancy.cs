namespace Core.Entities;
public class Vacancy
{
    public int Id { get; set; }
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
    public string CreatedById { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ExpiredAt { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public bool IsActive() => DateTime.Now <= ExpiredAt && ApplicationCount < MaxApplications && DeactivatedAt is null;
    public AppUser CreatedBy { get; set; } = null!;
    public ICollection<Application> Applications { get; set; } = [];//Not To Be Null
}