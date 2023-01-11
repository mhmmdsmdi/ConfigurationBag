using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Property : Entity
{
    public long ConfigurationId { get; set; }

    [Required]
    [StringLength(256)]
    public string Key { get; set; }

    [StringLength(256)]
    public string Description { get; set; }

    [ForeignKey(nameof(ConfigurationId))]
    public virtual Configuration Configuration { get; set; }

    public virtual ICollection<Value> Values { get; set; }
}

public class PropertyInsertDto : BaseDto<PropertyInsertDto, Property>
{
    public long CollectionId { get; set; }

    public string Description { get; set; }

    public string Key { get; set; }
}

public class PropertySelectDto : BaseDtoWithIdentity<PropertySelectDto, Property>
{
    public long CollectionId { get; set; }

    public string Description { get; set; }

    public string Key { get; set; }
}

public class PropertyInsertValidator : AbstractValidator<PropertyInsertDto>
{
    public PropertyInsertValidator()
    {
        RuleFor(x => x.CollectionId).NotEmpty();

        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Key).MaximumLength(256);

        RuleFor(x => x.Description).MaximumLength(256);
    }
}