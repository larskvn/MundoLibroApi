namespace Library.Domain.Models;

public class Loan
{
    public int Id { get; set; }  
    public DateTime LoanDate { get; set; } = DateTime.Now;  
    public DateTime? ReturnDate { get; set; }  
    public int LoanStatus { get; set; } = 0;  
    public int ApplicantId { get; set; }  
    public DateTime RegistrationDate { get; set; } = DateTime.Now;  
    public int Status { get; set; } = 1; 

    public virtual Applicant? Applicant { get; set; }
    public virtual ICollection<LoanDetail> LoanDetails { get; set; } = new List<LoanDetail>();

}