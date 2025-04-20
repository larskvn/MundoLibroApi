namespace BookNest.Application.Dtos.Editorial;

public class EditorialDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int Status { get; set; }
}