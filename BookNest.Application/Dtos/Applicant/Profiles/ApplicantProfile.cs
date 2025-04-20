namespace BookNest.Application.Dtos.Applicant.Profiles;

using AutoMapper;
using Library.Domain.Models;

public class ApplicantProfile: Profile
{
    public ApplicantProfile()
    {
        CreateMap<Applicant, ApplicantDto>();
        CreateMap<Applicant, ApplicantSmallDto>();
        CreateMap<Applicant, ApplicantWithLoansDto>(); 
        CreateMap<Applicant, ApplicantBodyDto>().ReverseMap(); 
    }
    
}