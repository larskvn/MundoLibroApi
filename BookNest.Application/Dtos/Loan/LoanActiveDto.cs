using BookNest.Application.Dtos.Applicant;

namespace BookNest.Application.Dtos.Loan;

public class LoanActiveDto
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int LoanStatus { get; set; } 
    public ApplicantSmallDto Applicant { get; set; }
}