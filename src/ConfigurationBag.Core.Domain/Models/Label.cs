using System.ComponentModel.DataAnnotations;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Label : Entity
{
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual ICollection<Value> Values { get; set; }

    public virtual ICollection<FeatureFlag> FeatureFlags { get; set; }
}

public class LabelInsertDto : BaseDto<LabelInsertDto, Label>
{
    public string Name { get; set; }
}

public class LabelSelectDto : BaseDtoWithIdentity<LabelSelectDto, Label>
{
    public string Name { get; set; }
}

public class LabelInsertValidator : AbstractValidator<LabelInsertDto>
{
    public LabelInsertValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}