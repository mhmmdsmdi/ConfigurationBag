using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class FeatureFlag : Entity
{
    public long CollectionId { get; set; }

    [Required]
    [StringLength(256)]
    public string Key { get; set; }

    [Required]
    [DefaultValue(true)]
    public bool IsEnable { get; set; }

    [StringLength(256)]
    public string Description { get; set; }

    [ForeignKey(nameof(CollectionId))]
    public virtual Collection Collection { get; set; }

    public virtual ICollection<Label> Labels { get; set; }
}

public class FeatureFlagInsertDto : BaseDto<FeatureFlagInsertDto, FeatureFlag>
{
    public long CollectionId { get; set; }

    public string Key { get; set; }

    public bool IsEnable { get; set; }

    public string Description { get; set; }
}

public class FeatureFlagSelectDto : BaseDtoWithIdentity<FeatureFlagSelectDto, FeatureFlag>
{
    public long CollectionId { get; set; }

    public string Key { get; set; }

    public bool IsEnable { get; set; }

    public string Description { get; set; }

    public ICollection<LabelSelectDto> Labels { get; set; }
}

public class FeatureFlagInsertValidator : AbstractValidator<FeatureFlagInsertDto>
{
    public FeatureFlagInsertValidator()
    {
        RuleFor(x => x.CollectionId).NotEmpty();

        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Key).MaximumLength(256);

        RuleFor(x => x.Description).MaximumLength(256);
    }
}