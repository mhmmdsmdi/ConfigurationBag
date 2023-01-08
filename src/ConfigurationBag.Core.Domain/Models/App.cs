using System.ComponentModel.DataAnnotations;
using ConfigurationBag.Core.Common.Dto;
using ConfigurationBag.Core.Common.Entities;

namespace ConfigurationBag.Core.Domain.Models;

public class App : Entity
{
    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    public virtual ICollection<Configuration> Configurations { get; set; }
}

public class AppInsertDto : BaseDto<AppInsertDto, App>
{
    public string Name { get; set; }
}

public class AppSelectDto : BaseDtoWithIdentity<AppSelectDto, App>
{
    public string Name { get; set; }
}