namespace BookNest.Application.Dtos.Applicant;

using Loan;

public class ApplicantDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string IdentityDocument { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int Status { get; set; }
    
    public List<LoanSmallDto> Loans { get; set; } = new();
}