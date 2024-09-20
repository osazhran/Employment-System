using API.Extensions;
using API.Helpers;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize(Roles = "Employer")]
public class VacancyController(IVacancyService _vacancyService) : BaseController
{
    [HttpGet]
    [Cached(600)]
    public async Task<IActionResult> GetVacancies()
    {
        var result = await _vacancyService.GetVacanciesAsync();

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpGet("{id}")]
    [Cached(600)]
    public async Task<IActionResult> GetVacancy(int id)
    {
        var result = await _vacancyService.GetVacancyAsync(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPost]
    public async Task<ActionResult<VacancyResponse>> CreateVacancy(VacancyRequest model)
    {
        var result = await _vacancyService.CreateAsync(model, User);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVacancy(int id, VacancyRequest model)
    {
        var result = await _vacancyService.UpdateVacancyAsync(id, model);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> DeactivateVacancy(int id)
    {
        var result = await _vacancyService.DeactivateVacancyAsync(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacancy(int id)
    {
        var result = await _vacancyService.DeleteVacancyAsync(id);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblemOrSuccessMessage();
    }
}
