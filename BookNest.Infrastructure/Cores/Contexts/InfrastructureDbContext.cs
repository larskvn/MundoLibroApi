using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Infrastructure.Cores.Contexts;

public class InfrastructureDbContext :DbContext
{
    public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext>options): base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}