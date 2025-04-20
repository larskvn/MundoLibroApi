using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BookNest.Application.Cores.Contexts;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        //Añadimos la configuracion de autoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
}