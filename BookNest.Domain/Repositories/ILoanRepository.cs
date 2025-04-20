using Library.Domain.Cores;
using Library.Domain.Models;

namespace Library.Domain.Repositories;

public interface ILoanRepository:ICrudRepository<Loan,int>
{
    
}