namespace Core.Entities;
public class Application
{
    public int VacancyId { get; set; }
    public Vacancy Vacancy { get; set; } = null!;
    public string ApplicantId { get; set; } = null!;
    public AppUser Applicant { get; set; } = null!;
    public DateTime AppliedAt { get; set; } = DateTime.Now;
    public string ResumeUrl { get; set; } = null!;
    public bool IsActive() => DateTime.UtcNow <= AppliedAt.AddHours(24);
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
}