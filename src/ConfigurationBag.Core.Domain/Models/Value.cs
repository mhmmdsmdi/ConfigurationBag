using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Value : Entity
{
    public long PropertyId { get; set; }

    public string Data { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey(nameof(PropertyId))]
    public virtual Property Property { get; set; }

    public virtual ICollection<Label> Labels { get; set; }
}

public class ValueInsertDto : BaseDto<ValueInsertDto, Value>
{
    public long PropertyId { get; set; }

    public string Data { get; set; }
}

public class ValueSelectDto : BaseDtoWithIdentity<ValueSelectDto, Value>
{
    public long PropertyId { get; set; }

    public string Data { get; set; }

    public DateTime Date { get; set; }

    public ICollection<LabelSelectDto> Labels { get; set; }
}

public class ValueInsertValidator : AbstractValidator<ValueInsertDto>
{
    public ValueInsertValidator()
    {
        RuleFor(x => x.PropertyId).NotEmpty();

        RuleFor(x => x.Data).NotEmpty();
    }
}