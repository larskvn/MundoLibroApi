namespace BookNest.Application.Services;

using Dtos.Loan;

public interface ILoanService
{
    Task<IReadOnlyList<LoanSmallDto>> FindAllAsync();
    Task<LoanDto> FindByIdAsync(int id);
    Task<IReadOnlyList<LoanActiveDto>> FindActiveAsync();
    Task<IReadOnlyList<LoanOverdueDto>> FindOverdueAsync();
    Task<LoanDto> CreateAsync(LoanBodyDto dto);
    Task<LoanReturnDto> ReturnAsync(int id, LoanReturnDto dto);
    Task<LoanSmallDto> ExtendAsync(int id, LoanExtendDto dto);
}