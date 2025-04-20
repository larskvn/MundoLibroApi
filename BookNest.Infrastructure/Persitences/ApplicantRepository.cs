using BookNest.Infrastructure.Cores.Contexts;
using BookNest.Infrastructure.Cores.Persistences;
using Library.Domain.Models;
using Library.Domain.Repositories;

namespace BookNest.Infrastructure.Persitences;

public class ApplicantRepository: CrudRepository<Applicant, int>, IApplicantRepository
{
    public ApplicantRepository(InfrastructureDbContext context) : base(context)
    {
    }
}