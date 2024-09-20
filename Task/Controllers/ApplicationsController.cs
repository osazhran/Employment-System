using API.Extensions;
using API.Helpers;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ApplicationsController(IApplicationService _applicationService) : BaseController
{
    [HttpGet]
    [Authorize(Roles = "Employer")]
    [Cached(600)]
    public async Task<IActionResult> GetApplications()
    {
        var result = await _applicationService.GetApplicationsAsync();

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpGet("user")]
    [Cached(600)]
    public async Task<IActionResult> GetUserApplications()
    {
        var result = await _applicationService.GetUserApplicationsAsync(User);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpGet("vacancy/{id}")]
    [Authorize(Roles = "Employer")]
    [Cached(600)]
    public async Task<IActionResult> GetVacancyApplications(int id)
    {
        var result = await _applicationService.GetVacancyApplicationsAsync(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }


    [HttpPost]
    public async Task<IActionResult> CreateApplication(ApplicationRequest model)
    {
        var result = await _applicationService.CreateAsync(model, User);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPut("accept")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> AcceptApplication(AcceptAndRegectRequest request)
    {
        var result = await _applicationService.AcceptAsync(request.ApplicantId, request.VacancyId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPut("reject")]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> RejectApplication(AcceptAndRegectRequest request)
    {
        var result = await _applicationService.RejectAsync(request.ApplicantId, request.VacancyId);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

}