using System.ComponentModel.DataAnnotations;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using ConfigurationBag.Core.Common.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationBag.Core.Domain.Models;

public class Collection : Entity
{
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual ICollection<Configuration> Configurations { get; set; }
    public virtual ICollection<FeatureFlag> FeatureFlags { get; set; }
}

public class CollectionInsertDto : BaseDto<CollectionInsertDto, Collection>
{
    public string Name { get; set; }
}

public class CollectionSelectDto : BaseDtoWithIdentity<CollectionSelectDto, Collection>
{
    public string Name { get; set; }
}

public class CollectionInsertValidator : AbstractValidator<CollectionInsertDto>
{
    private readonly IRepository<Collection> _repository;

    public CollectionInsertValidator(IRepository<Collection> repository)
    {
        _repository = repository;

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(256);
        RuleFor(x => x.Name).Must(UniqueName).WithMessage("This collection name already exists.");
    }

    private bool UniqueName(string value)
    {
        return _repository.TableNoTracking.All(x => x.Name == value);
    }
}