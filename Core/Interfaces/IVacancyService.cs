using Core.Dtos;
using Core.Entities;
using Core.ErrorHandling;
using System.Security.Claims;

namespace Core.Interfaces;
public interface IVacancyService
{
    Task<Result<IEnumerable<VacancyResponse>>> GetVacanciesAsync();
    Task<Result<VacancyResponse>> GetVacancyAsync(int id);
    Task<Result<VacancyResponse>> CreateAsync(VacancyRequest vacancy, ClaimsPrincipal user);
    Task<Result<VacancyResponse>> UpdateVacancyAsync(int id, VacancyRequest model);
    Task<Result<VacancyResponse>> DeactivateVacancyAsync(int id);
    Task<Result<VacancyResponse>> DeleteVacancyAsync(int id);
    Task<Vacancy?> GetVacancyForServiceAsync(int id);
}