namespace BookNest.Application.Dtos.Loan.Profiles;

using Applicant;
using AutoMapper;
using Library.Domain.Models;

public class LoanProfile :Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanDto>()
            .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant)); 

        CreateMap<Loan, LoanSmallDto>();
        CreateMap<Loan, LoanActiveDto>()
            .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant))
            .ForMember(dest => dest.LoanStatus, opt => opt.MapFrom(src => src.LoanStatus)); 
        CreateMap<Loan, LoanOverdueDto>()
            .ForMember(dest => dest.Applicant, opt => opt.MapFrom(src => src.Applicant)); 
 
        CreateMap<LoanBodyDto, Loan>().ReverseMap();
        CreateMap<Applicant, ApplicantSmallDto>();
    }
}