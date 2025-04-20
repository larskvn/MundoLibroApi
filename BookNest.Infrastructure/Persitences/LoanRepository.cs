using BookNest.Infrastructure.Cores.Contexts;
using BookNest.Infrastructure.Cores.Persistences;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Infrastructure.Persitences;

public class LoanRepository :CrudRepository<Loan,int>,ILoanRepository
{
    public LoanRepository(InfrastructureDbContext context) : base(context)
    {
    }
}