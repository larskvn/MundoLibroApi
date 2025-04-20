namespace BookNest.Application.Dtos.Loan;

public class LoanSmallDto
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int LoanStatus { get; set; }
}