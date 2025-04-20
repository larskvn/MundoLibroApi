namespace Library.Domain.Models;

public class Editorial
{
    public int Id { get; set; }
    public string? Code  { get; set; }
    public string? Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int Status { get; set; }
    
  
    public required virtual ICollection<Book> Books { get; set; }
}