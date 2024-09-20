using Core.Dtos;
using Core.ErrorHandling;
using System.Security.Claims;

namespace Core.Interfaces;
public interface IApplicationService
{
    Task<Result<IEnumerable<ApplicationResponse>>> GetApplicationsAsync();
    Task<Result<IEnumerable<ApplicationResponse>>> GetUserApplicationsAsync(ClaimsPrincipal user);
    Task<Result<IEnumerable<ApplicationResponse>>> GetVacancyApplicationsAsync(int id);
    Task<Result<ApplicationResponse>> CreateAsync(ApplicationRequest model, ClaimsPrincipal user);
    Task<Result<ApplicationResponse>> AcceptAsync(string applicantId, int vacancyId);
    Task<Result<ApplicationResponse>> RejectAsync(string applicantId, int vacancyId);
}