using System.ComponentModel.DataAnnotations;

namespace ConfigurationBag.Core.Common.Entities;

public class Entity
{
    protected Entity()
    { }

    [Key]
    public long Id { get; set; }
}