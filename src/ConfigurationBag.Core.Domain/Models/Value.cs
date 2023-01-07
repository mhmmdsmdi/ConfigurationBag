using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class Value : Entity
{
    public long PropertyId { get; set; }

    public string Data { get; set; }

    public DateTime Date { get; set; }

    [ForeignKey(nameof(PropertyId))]
    public virtual Property Property { get; set; }
}