namespace BookNest.Application.Services.Implemetation;

using System.Linq.Expressions;
using AutoMapper;
using Dtos.Loan;
using FluentValidation;
using Library.Domain.Models;
using Library.Domain.Repositories;

public class LoanService: ILoanService
{
    
    private readonly ILoanRepository  _loanRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<LoanBodyDto> _loanValidator;
    private readonly IValidator<LoanReturnDto> _loanReturnValidator;
    private readonly IValidator<LoanExtendDto> _loanExtendValidator;

    public LoanService(ILoanRepository loanRepository, IMapper mapper,IValidator<LoanBodyDto> loanValidator,IValidator<LoanReturnDto> loanReturnValidator,
        IValidator<LoanExtendDto> loanExtendValidator)
    {
        _loanRepository = loanRepository ;
        _mapper = mapper ;
        _loanValidator = loanValidator;
        _loanReturnValidator = loanReturnValidator;
        _loanExtendValidator = loanExtendValidator;

    }
   
    public async Task<IReadOnlyList<LoanSmallDto>> FindAllAsync()
    {
        var loans = await _loanRepository.FindAllAsync();
        return _mapper.Map<IReadOnlyList<LoanSmallDto>>(loans);
    }

    
    public async Task<LoanDto> FindByIdAsync(int id)
    {
        var includes = new List<Expression<Func<Loan, object>>>
        {
            l => l.Applicant
        };

        var loan = await _loanRepository.FindFirstOrDefaultAsync(l => l.Id == id, includes);

        if (loan == null)
            throw new Exception($"Préstamo con id {id} no encontrado");

        return _mapper.Map<LoanDto>(loan);
    }

    
    public async Task<IReadOnlyList<LoanActiveDto>> FindActiveAsync()
    {
        
        var loans = await _loanRepository.FindAllAsync(
            l => l.LoanStatus == 0 && l.Status == 1,
            new List<Expression<Func<Loan, object>>> { l => l.Applicant }
        );
        return _mapper.Map<IReadOnlyList<LoanActiveDto>>(loans);
    }


    
    public async Task<IReadOnlyList<LoanOverdueDto>> FindOverdueAsync()
    {
        var currentDate = DateTime.UtcNow;

        var overdueLoans = await _loanRepository.FindAllAsync(
            l => l.LoanStatus == 0 && 
                 l.ReturnDate < currentDate && 
                 l.Status == 1, 
            new List<Expression<Func<Loan, object>>> { l => l.Applicant }
        );

        var result = _mapper.Map<IReadOnlyList<LoanOverdueDto>>(overdueLoans);

        foreach (var loan in result)
        {
            if (loan.ReturnDate.HasValue)
            {
                loan.DaysOverdue = (currentDate - loan.ReturnDate.Value).Days;
            }
            else
            {
                loan.DaysOverdue = 0;
            }
        }

        return result;
    }
    
    public async Task<LoanDto> CreateAsync(LoanBodyDto dto)
    {
        var validationResult = await _loanValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var loan = new Loan
        {
            ApplicantId = dto.ApplicantId,
            LoanDate = DateTime.UtcNow,
            ReturnDate = dto.ReturnDate,
            LoanStatus = 0 
        };

        await _loanRepository.SaveAsync(loan);

        return _mapper.Map<LoanDto>(loan);
    }

    
    public async Task<LoanReturnDto> ReturnAsync(int id, LoanReturnDto dto)
    {
        var validationResult = await _loanReturnValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var loan = await _loanRepository.FindByIdAsync(id);
        if (loan == null)
            throw new Exception($"Préstamo con id {id} no encontrado");
        if (loan.LoanStatus == 1 || loan.LoanStatus == 2)
            throw new Exception("Este préstamo ya ha sido devuelto y no puede ser modificado.");

        loan.LoanStatus = dto.LoanStatus;
        loan.ReturnDate = DateTime.UtcNow;

        await _loanRepository.SaveAsync(loan);

        return dto;
    }

    
    public async Task<LoanSmallDto> ExtendAsync(int id, LoanExtendDto dto)
    {
        var validationResult = await _loanExtendValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var loan = await _loanRepository.FindByIdAsync(id);
        if (loan == null)
            throw new Exception($"Préstamo con id {id} no encontrado");

        loan.ReturnDate = dto.NewReturnDate;

        await _loanRepository.SaveAsync(loan);

        return _mapper.Map<LoanSmallDto>(loan);
    }
}