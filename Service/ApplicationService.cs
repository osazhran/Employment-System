using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.ErrorHandling;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Specifications;
using System.Security.Claims;
namespace Service;
public class ApplicationService(IMapper _mapper, IUnitOfWork _unitOfWork, IAccountService _accountService, IVacancyService _vacancyService) : IApplicationService
{
    public async Task<Result<IEnumerable<ApplicationResponse>>> GetApplicationsAsync()
    {
        var applicationSpec = new BaseSpecifications<Application>(a => a.Applicant);

        var applications = await _unitOfWork.Repository<Application>().GetAllAsync(applicationSpec);

        var applicationResponses = _mapper.Map<IEnumerable<Application>, IEnumerable<ApplicationResponse>>(applications);

        return Result.Success(applicationResponses);
    }

    public async Task<Result<IEnumerable<ApplicationResponse>>> GetUserApplicationsAsync(ClaimsPrincipal user)
    {
        var currentUser = await _accountService.GetCurrentUser(user);

        var applicationSpec = new BaseSpecifications<Application>(a => a.ApplicantId == currentUser.Id, a => a.Applicant);

        var applications = await _unitOfWork.Repository<Application>().GetAllAsync(applicationSpec);

        var applicationResponses = _mapper.Map<IEnumerable<Application>, IEnumerable<ApplicationResponse>>(applications);

        return Result.Success(applicationResponses);
    }

    public async Task<Result<IEnumerable<ApplicationResponse>>> GetVacancyApplicationsAsync(int id)
    {
        var vacancy = await _vacancyService.GetVacancyForServiceAsync(id);

        if (vacancy is null)
            return Result.Failure<IEnumerable<ApplicationResponse>>(404, "Vacancy not found");

        var applicationSpec = new BaseSpecifications<Application>(a => a.VacancyId == id, a => a.Applicant);

        var applications = await _unitOfWork.Repository<Application>().GetAllAsync(applicationSpec);

        var applicationResponses = _mapper.Map<IEnumerable<Application>, IEnumerable<ApplicationResponse>>(applications);

        return Result.Success(applicationResponses);
    }

    public async Task<Result<ApplicationResponse>> CreateAsync(ApplicationRequest model, ClaimsPrincipal user)
    {
        var applicationRepo = _unitOfWork.Repository<Application>();

        var application = _mapper.Map<ApplicationRequest, Application>(model);

        var currentUser = await _accountService.GetCurrentUser(user);

        var applicationSpec = new BaseSpecifications<Application>(a => a.ApplicantId == currentUser.Id);

        var userApplication = await applicationRepo.GetAllAsync(applicationSpec);

        var existingActiveApplication = userApplication.Where(a => a.IsActive()).FirstOrDefault();

        if (existingActiveApplication is not null)
            return Result.Failure<ApplicationResponse>(400, "You have already applied for a vacancy within the last 24 hours");

        var vacancy = await _vacancyService.GetVacancyForServiceAsync(application.VacancyId);

        if (vacancy is null)
            return Result.Failure<ApplicationResponse>(404, "Vacancy not found");

        if (vacancy.IsActive() is false)
            return Result.Failure<ApplicationResponse>(400, "Vacancy is not active");

        vacancy.ApplicationCount++;

        var result = await _vacancyService.UpdateVacancyAsync(vacancy.Id, _mapper.Map<Vacancy, VacancyRequest>(vacancy));

        if (result.IsSuccess is false)
            return Result.Failure<ApplicationResponse>(400, "Failed to create application");

        application.ApplicantId = currentUser.Id;
        application.Applicant = currentUser;
        application.Vacancy = vacancy;

        await applicationRepo.AddAsync(application);

        var saveResult = await _unitOfWork.CompleteAsync();

        if (saveResult <= 0)
            return Result.Failure<ApplicationResponse>(400, "Failed to create application");

        var applicationResponse = _mapper.Map<Application, ApplicationResponse>(application);

        return Result.Success(applicationResponse);
    }

    public async Task<Result<ApplicationResponse>> AcceptAsync(string applicantId, int vacancyId)
    {
        var applicationRepo = _unitOfWork.Repository<Application>();

        var applicationSpec = new BaseSpecifications<Application>(a => a.ApplicantId == applicantId && a.VacancyId == vacancyId, a => a.Applicant);

        var application = await applicationRepo.GetEntityAsync(applicationSpec);

        if (application is null)
            return Result.Failure<ApplicationResponse>(404, "Application not found");

        var vacancy = await _vacancyService.GetVacancyForServiceAsync(application.VacancyId);

        if (vacancy is null)
            return Result.Failure<ApplicationResponse>(404, "Vacancy not found");

        if (vacancy.IsActive() is false)
            return Result.Failure<ApplicationResponse>(400, "Vacancy is not active");

        if (application.IsActive() is false)
            return Result.Failure<ApplicationResponse>(400, "Application is not active");

        application.Status = ApplicationStatus.Accepted;

        applicationRepo.Update(application);

        var saveResult = await _unitOfWork.CompleteAsync();

        if (saveResult <= 0)
            return Result.Failure<ApplicationResponse>(400, "Failed to accept application");

        var applicationResponse = _mapper.Map<Application, ApplicationResponse>(application);

        return Result.Success(applicationResponse);
    }

    public async Task<Result<ApplicationResponse>> RejectAsync(string applicantId, int vacancyId)
    {
        var applicationRepo = _unitOfWork.Repository<Application>();

        var applicationSpec = new BaseSpecifications<Application>(a => a.ApplicantId == applicantId && a.VacancyId == vacancyId, a => a.Applicant);

        var application = await applicationRepo.GetEntityAsync(applicationSpec);

        if (application is null)
            return Result.Failure<ApplicationResponse>(404, "Application not found");

        var vacancy = await _vacancyService.GetVacancyForServiceAsync(application.VacancyId);

        if (vacancy is null)
            return Result.Failure<ApplicationResponse>(404, "Vacancy not found");

        if (vacancy.IsActive() is false)
            return Result.Failure<ApplicationResponse>(400, "Vacancy is not active");

        if (application.IsActive() is false)
            return Result.Failure<ApplicationResponse>(400, "Application is not active");

        application.Status = ApplicationStatus.Rejected;

        applicationRepo.Update(application);

        var saveResult = await _unitOfWork.CompleteAsync();

        if (saveResult <= 0)
            return Result.Failure<ApplicationResponse>(400, "Failed to reject application");

        var applicationResponse = _mapper.Map<Application, ApplicationResponse>(application);

        return Result.Success(applicationResponse);
    }

}