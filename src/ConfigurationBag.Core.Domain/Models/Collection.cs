using System.ComponentModel.DataAnnotations;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class Collection : Entity
{
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual ICollection<Configuration> Configurations { get; set; }
}

public class CollectionInsertDto : BaseDto<CollectionInsertDto, Collection>
{
    public string Name { get; set; }
}

public class CollectionSelectDto : BaseDtoWithIdentity<CollectionSelectDto, Collection>
{
    public string Name { get; set; }
}