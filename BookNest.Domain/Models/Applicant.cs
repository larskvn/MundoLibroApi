namespace Library.Domain.Models;

public class Applicant
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? IdentityDocument {get; set;}
    public string? Email { get; set; }  
    public string? Phone { get; set; }  
    public DateTime RegistrationDate { get; set; } = DateTime.Now;  
    public int Status { get; set; } = 1;  
    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}