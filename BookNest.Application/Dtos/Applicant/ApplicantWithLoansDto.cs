using BookNest.Application.Dtos.Loan;

namespace BookNest.Application.Dtos.Applicant;

public class ApplicantWithLoansDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public List<LoanSmallDto> Loans { get; set; }
}