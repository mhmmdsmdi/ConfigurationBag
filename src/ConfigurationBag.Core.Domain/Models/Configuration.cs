using System.ComponentModel.DataAnnotations;
using ConfigurationBag.Core.Common.Entities;
using FluentValidation;

namespace ConfigurationBag.Core.Domain.Models;

public class Configuration : Entity
{
    [Required]
    [StringLength(256)]
    public string Key { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}

public class ConfigurationValidator : AbstractValidator<Configuration>
{
    public ConfigurationValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.Key).MaximumLength(256);

        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(256);
    }
}