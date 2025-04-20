namespace BookNest.Application.Services.Implemetation;

using System.Linq.Expressions;
using AutoMapper;
using Dtos.Applicant;
using Dtos.Loan;
using FluentValidation;
using Library.Domain.Models;
using Library.Domain.Repositories;

public class ApplicantService: IApplicantService
{
    
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILoanService _loanService;
    private readonly IMapper _mapper;
    private readonly IValidator<ApplicantBodyDto> _applicantValidator;
    

    public ApplicantService(IApplicantRepository applicantRepository,ILoanService loanService, IMapper mapper, IValidator<ApplicantBodyDto> applicantValidator)
    {
        _applicantRepository = applicantRepository;
        _loanService = loanService;
        _mapper = mapper;
        _applicantValidator = applicantValidator;
        
    }
    public async Task<IReadOnlyList<ApplicantSmallDto>> FindAllAsync()
    {
        var applicants = await _applicantRepository.FindAllAsync();
        return _mapper.Map<IReadOnlyList<ApplicantSmallDto>>(applicants);
    }

    public async Task<ApplicantDto> FindByIdAsync(int id)
    {
        var applicant = await _applicantRepository.FindByIdAsync(id);
        if (applicant is null)
            throw new Exception($"Solicitante con id {id} no encontrado");

        return _mapper.Map<ApplicantDto>(applicant);
    }

    public async Task<ApplicantWithLoansDto> FindWithLoansAsync(int id)
    {
        var includes = new List<Expression<Func<Applicant, object>>>
        {
            a => a.Loans
        };

        var applicant = await _applicantRepository.FindFirstOrDefaultAsync(a => a.Id == id, includes);
        if (applicant is null)
            throw new Exception($"Solicitante con id {id} no encontrado");

        return _mapper.Map<ApplicantWithLoansDto>(applicant);
    }

    public async Task<ApplicantDto> CreateAsync(ApplicantBodyDto dto)
    {
        var validationResult = await _applicantValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var applicant = _mapper.Map<Applicant>(dto);
        applicant.RegistrationDate = DateTime.UtcNow;
        applicant.Status = 1;

        await _applicantRepository.SaveAsync(applicant);

        var loanDto = new LoanBodyDto
        {
            ApplicantId = applicant.Id,
            ReturnDate = DateTime.UtcNow.AddDays(30)  
        };

        var loan = await _loanService.CreateAsync(loanDto); 

        var applicantDto = _mapper.Map<ApplicantDto>(applicant);

        applicantDto.Loans = new List<LoanSmallDto>
        {
            new LoanSmallDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,
                LoanStatus = loan.LoanStatus
            }
        };

        return applicantDto;
    }

    public async Task<ApplicantDto> UpdateAsync(int id, ApplicantBodyDto dto)
    {
        var validationResult = await _applicantValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var applicant = await _applicantRepository.FindByIdAsync(id);
        if (applicant is null)
            throw new Exception($"Solicitante con id {id} no encontrado");

        applicant.FullName = dto.FullName;
        applicant.Email = dto.Email;
        applicant.Phone = dto.Phone;
        applicant.IdentityDocument = dto.IdentityDocument;

        await _applicantRepository.SaveAsync(applicant);

        return _mapper.Map<ApplicantDto>(applicant);
    }
}