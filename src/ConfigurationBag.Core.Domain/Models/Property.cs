using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class Property : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Value> Values { get; set; }
}