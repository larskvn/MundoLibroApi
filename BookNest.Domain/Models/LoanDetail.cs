namespace Library.Domain.Models;

public class LoanDetail
{
    public int LoanId { get; set; }          
    public int BookId { get; set; }          
    public bool IsReturned { get; set; } = false;  
    public decimal Penalty { get; set; } = 0m;     
    public DateTime RegistrationDate { get; set; } = DateTime.Now;  
    public int Status { get; set; } = 1;       
    
    
    public virtual Loan? Loan { get; set; }
    
   
    public virtual Book? Book{ get; set; }
}