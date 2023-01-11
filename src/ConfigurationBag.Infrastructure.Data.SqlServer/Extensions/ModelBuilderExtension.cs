using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pluralize.NET;

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Extensions;

public static class ModelBuilderExtension
{
    /// <summary>
    /// Dynamical load all IEntityTypeConfiguration with Reflection
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies"></param>
    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var applyGenericMethod = typeof(ModelBuilder).GetMethods()
            .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic);

        foreach (var type in types)
            foreach (var interfaceItem in type.GetInterfaces())
                if (interfaceItem.IsConstructedGenericType &&
                    interfaceItem.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(interfaceItem.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                }
    }

    /// <summary>
    /// Pluralizing table name like Post to Posts or Person to People
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        var pluralizer = new Pluralizer();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            entityType.SetTableName(pluralizer.Pluralize(tableName));
        }
    }
}