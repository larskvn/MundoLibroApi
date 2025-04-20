using BookNest.Infrastructure.Cores.Contexts;
using BookNest.Infrastructure.Cores.Persistences;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Infrastructure.Persitences;

public class EditorialRepository: CrudRepository<Editorial,int>, IEditorialRepository
{
    public EditorialRepository(InfrastructureDbContext context) : base(context)
    {
    }
}