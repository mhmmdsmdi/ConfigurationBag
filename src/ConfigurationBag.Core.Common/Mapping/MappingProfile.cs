using AutoMapper;

namespace ConfigurationBag.Core.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile(IEnumerable<ICustomMapping> haveCustomMappings)
    {
        foreach (var item in haveCustomMappings)
            item.CreateMappings(this);
    }
}