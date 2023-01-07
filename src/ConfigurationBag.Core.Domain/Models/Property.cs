using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Property : Entity
{
    public long ConfigurationId { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    [ForeignKey(nameof(ConfigurationId))]
    public virtual Configuration Configuration { get; set; }

    public virtual ICollection<Value> Values { get; set; }
}

public class PropertyValidator : AbstractValidator<Property>
{
    public PropertyValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(256);
    }
}