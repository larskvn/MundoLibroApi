using BookNest.Infrastructure.Cores.Contexts;
using BookNest.Infrastructure.Cores.Persistences;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Infrastructure.Persitences;

public class LoanDetailRepository :CrudRepository<LoanDetail,int>,ILoanDetailRepository
{
    public LoanDetailRepository(InfrastructureDbContext context) : base(context)
    {
    }
}