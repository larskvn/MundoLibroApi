using Library.Domain.Cores;
using Library.Domain.Models;

namespace Library.Domain.Repositories;

public interface IBookRepository:ICrudRepository<Book,int>
{
    
}