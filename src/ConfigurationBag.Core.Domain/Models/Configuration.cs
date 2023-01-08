using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Configuration : Entity
{
    public long CollectionId { get; set; }

    [Required]
    [StringLength(256)]
    public string Key { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    [ForeignKey(nameof(CollectionId))]
    public virtual Collection Collection { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}

public class ConfigurationInsertDto : BaseDto<ConfigurationInsertDto, Configuration>
{
    public long CollectionId { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }
}

public class ConfigurationSelectDto : BaseDtoWithIdentity<ConfigurationSelectDto, Configuration>
{
    public long CollectionId { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }
}

public class ConfigurationInsertValidator : AbstractValidator<ConfigurationInsertDto>
{
    public ConfigurationInsertValidator()
    {
        RuleFor(x => x.CollectionId).NotEmpty();

        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Key).MaximumLength(256);

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(256);
    }
}