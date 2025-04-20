namespace BookNest.Application.Services;

using Dtos.Applicant;

public interface IApplicantService
{
    Task<IReadOnlyList<ApplicantSmallDto>> FindAllAsync();
    Task<ApplicantDto> FindByIdAsync(int id);
    Task<ApplicantWithLoansDto> FindWithLoansAsync(int id);
    Task<ApplicantDto> CreateAsync(ApplicantBodyDto dto);
    Task<ApplicantDto> UpdateAsync(int id, ApplicantBodyDto dto);
}