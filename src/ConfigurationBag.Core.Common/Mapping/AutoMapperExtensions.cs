using AutoMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationBag.Core.Common.Mapping;

public static class AutoMapperExtensions
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddAutoMapper(config =>
        {
            config.AddMappingProfile(assemblies);
        }, assemblies);
    }

    public static void AddMappingProfile(this IMapperConfigurationExpression config,
        params Assembly[] assemblies)
    {
        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                                          type.GetInterfaces().Contains(typeof(ICustomMapping)))
            .Select(type => (ICustomMapping)Activator.CreateInstance(type));

        var profile = new MappingProfile(list);

        config.AddProfile(profile);
    }
}