using AutoMapper;

namespace ConfigurationBag.Core.Common.Mapping;

public interface ICustomMapping
{
    void CreateMappings(Profile profile);
}