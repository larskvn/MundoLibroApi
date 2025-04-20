using BookNest.Infrastructure.Cores.Contexts;
using BookNest.Infrastructure.Cores.Persistences;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Infrastructure.Persitences;

public class BookRepository : CrudRepository<Book, int>, IBookRepository
{
    public BookRepository(InfrastructureDbContext context) : base(context)
    {
    }
}