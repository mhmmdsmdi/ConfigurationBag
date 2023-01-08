using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using AutoMapper;
using ConfigurationBag.Core.Common.Entities;
using ConfigurationBag.Core.Common.Mapping;

namespace ConfigurationBag.Core.Common.Dto;

public abstract class BaseDto<TDto, TEntity> : ICustomMapping
    where TDto : class, new()
    where TEntity : Entity, new()
{
    public TEntity ToEntity(IMapper mapper)
    {
        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
    }

    public TEntity ToEntity(IMapper mapper, TEntity entity)
    {
        return mapper.Map(CastToDerivedClass(mapper, this), entity);
    }

    public static TDto FromEntity(IMapper mapper, TEntity model)
    {
        return mapper.Map<TDto>(model);
    }

    protected TDto CastToDerivedClass(IMapper mapper, BaseDto<TDto, TEntity> baseInstance)
    {
        return mapper.Map<TDto>(baseInstance);
    }

    public T MapTo<T>()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TDto, T>();
        });

        return configuration.CreateMapper().Map<T>(this);
    }

    public void CreateMappings(Profile profile)
    {
        var mappingExpression = profile.CreateMap<TDto, TEntity>();

        var dtoType = typeof(TDto);
        var entityType = typeof(TEntity);
        //Ignore any property of source (like Post.Author) that dose not contains in destination
        var properties = entityType.GetProperties();
        foreach (var property in properties)
        {
            if (dtoType.GetProperties().FirstOrDefault(x => x.Name == property.Name) == null)
                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
        }

        CustomMappings(mappingExpression.ReverseMap());
    }

    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping)
    {
    }
}


public abstract class BaseDtoWithIdentity<TDto, TEntity> : BaseDto<TDto, TEntity>
    where TDto : class, new()
    where TEntity : Entity, new()
{
    public long Id { get; set; }
}