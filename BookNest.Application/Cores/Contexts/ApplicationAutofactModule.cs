using System.Reflection;
using Autofac;

namespace BookNest.Application.Cores.Contexts;

public class ApplicationAutofactModule:Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    } 
}