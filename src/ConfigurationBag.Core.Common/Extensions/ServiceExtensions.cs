using System.Reflection;
using Autofac;
using ConfigurationBag.Core.Common.LifeTimes;

namespace ConfigurationBag.Core.Common.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this ContainerBuilder containerBuilder, Type repositoryType,
        Type iRepositoryType, params Assembly[] assemblies)
    {
        //RegisterType > As > Lifetime
        containerBuilder.RegisterGeneric(repositoryType).As(iRepositoryType);

        // Add scoped services to dependency
        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        // Add transient services to dependency
        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .InstancePerDependency();

        // Add singleton services to dependency
        containerBuilder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}