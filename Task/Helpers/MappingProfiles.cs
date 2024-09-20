using AutoMapper;
using Core.Dtos;
using Core.Entities;

namespace DotNetCore_ECommerce.Helpers;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterRequest, AppUser>();

        CreateMap<VacancyRequest, Vacancy>();

        CreateMap<Vacancy, VacancyRequest>();

        CreateMap<Vacancy, VacancyResponse>()
            .ForMember(dest => dest.ExpiredAt, opt => opt.MapFrom(src => src.ExpiredAt.ToString("MM/dd/yyyy hh:mm tt")));

        CreateMap<ApplicationRequest, Application>();

        CreateMap<Application, ApplicationResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Applicant.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Applicant.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Applicant.Email))
            .ForMember(dest => dest.AppliedAt, opt => opt.MapFrom(src => src.AppliedAt.ToString("MM/dd/yyyy hh:mm tt")));
    }
}