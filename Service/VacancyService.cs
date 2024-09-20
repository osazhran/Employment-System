using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.ErrorHandling;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using System.Security.Claims;
namespace Service;
public class VacancyService(IAccountService _accountService, IMapper _mapper, IUnitOfWork _unitOfWork) : IVacancyService
{
    public async Task<Result<IEnumerable<VacancyResponse>>> GetVacanciesAsync()
    {
        var vacancies = await _unitOfWork.Repository<Vacancy>().GetAllAsync();

        var vacancyResponses = _mapper.Map<IEnumerable<Vacancy>, IEnumerable<VacancyResponse>>(vacancies);

        return Result.Success(vacancyResponses);
    }
    public async Task<Result<VacancyResponse>> GetVacancyAsync(int id)
    {
        var vacancy = await GetVacancyForServiceAsync(id);

        if (vacancy == null)
            return Result.Failure<VacancyResponse>(404, "Vacancy not found");

        var vacancyResponse = _mapper.Map<Vacancy, VacancyResponse>(vacancy);

        return Result.Success(vacancyResponse);
    }
    public async Task<Result<VacancyResponse>> CreateAsync(VacancyRequest vacancy, ClaimsPrincipal user)
    {
        var currentUser = await _accountService.GetCurrentUser(user);

        var newVacancy = _mapper.Map<VacancyRequest, Vacancy>(vacancy);

        newVacancy.CreatedBy = currentUser;
        newVacancy.CreatedById = currentUser.Id;

        await _unitOfWork.Repository<Vacancy>().AddAsync(newVacancy);

        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
            return Result.Failure<VacancyResponse>(400, "Failed to create vacancy");

        var vacancyResponse = _mapper.Map<Vacancy, VacancyResponse>(newVacancy);

        vacancyResponse.ExpiredAt = vacancy.ExpiredAt.ToString("MM/dd/yyyy hh:mm tt");

        return Result.Success(vacancyResponse);
    }
    public async Task<Result<VacancyResponse>> UpdateVacancyAsync(int id, VacancyRequest model)
    {
        var vacancyRepo = _unitOfWork.Repository<Vacancy>();

        var vacancy = await vacancyRepo.GetAsync(id);

        if (vacancy == null)
            return Result.Failure<VacancyResponse>(404, "Vacancy not found");

        var vacancyToSave = _mapper.Map(model, vacancy);

        vacancyToSave.CreatedAt = vacancy.CreatedAt;

        vacancyToSave.CreatedById = vacancy.CreatedById;

        vacancyToSave.ApplicationCount = vacancy.ApplicationCount;

        vacancyRepo.Update(vacancyToSave);

        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
            return Result.Failure<VacancyResponse>(400, "Failed to update vacancy");

        var vacancyResponse = _mapper.Map<Vacancy, VacancyResponse>(vacancyToSave);

        return Result.Success(vacancyResponse);
    }
    public async Task<Result<VacancyResponse>> DeactivateVacancyAsync(int id)
    {
        var vacancyRepo = _unitOfWork.Repository<Vacancy>();

        var vacancy = await vacancyRepo.GetAsync(id);

        if (vacancy == null)
            return Result.Failure<VacancyResponse>(404, "Vacancy not found");

        vacancy.DeactivatedAt = DateTime.Now;

        vacancyRepo.Update(vacancy);

        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
            return Result.Failure<VacancyResponse>(400, "Failed to deactivate vacancy");

        var vacancyResponse = _mapper.Map<Vacancy, VacancyResponse>(vacancy);

        return Result.Success(vacancyResponse);
    }
    public async Task<Result<VacancyResponse>> DeleteVacancyAsync(int id)
    {
        var vacancyRepo = _unitOfWork.Repository<Vacancy>();

        var vacancy = await vacancyRepo.GetAsync(id);

        if (vacancy == null)
            return Result.Failure<VacancyResponse>(404, "Vacancy not found");

        vacancyRepo.Delete(vacancy);

        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
            return Result.Failure<VacancyResponse>(400, "Failed to delete vacancy");

        var vacancyResponse = _mapper.Map<Vacancy, VacancyResponse>(vacancy);

        return Result.Success(vacancyResponse);
    }
    public async Task<Vacancy?> GetVacancyForServiceAsync(int id)
    {
        var vacancy = await _unitOfWork.Repository<Vacancy>().GetAsync(id);

        if (vacancy == null)
            return null;

        return vacancy;
    }

}