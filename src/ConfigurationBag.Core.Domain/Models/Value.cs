using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class Value : Entity
{
    public string Data { get; set; }

    public DateTime Date { get; set; }
}