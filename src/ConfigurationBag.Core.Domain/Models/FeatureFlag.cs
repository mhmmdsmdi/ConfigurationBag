using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConfigurationBag.Core.Common.Entities;

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
}