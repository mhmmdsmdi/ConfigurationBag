using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class Configuration : Entity
{
    public string Key { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}