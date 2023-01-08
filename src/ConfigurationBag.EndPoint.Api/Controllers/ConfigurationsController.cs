using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

/// <summary>
/// Configurations
/// </summary>
public class ConfigurationsController : BaseApiController
{
    private readonly IConfigurationService _service;

    public ConfigurationsController(IConfigurationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get all configurations
    /// </summary>
    /// <param name="collectionId">Collection Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{collectionId}")]
    public async Task<ICollection<ConfigurationSelectDto>> Get(long collectionId, CancellationToken cancellationToken)
    {
        return await _service.Get(collectionId, cancellationToken);
    }

    /// <summary>
    /// Create configuration
    /// </summary>
    /// <param name="configuration">Configuration</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ConfigurationSelectDto> InsertAsync(ConfigurationInsertDto configuration,
        CancellationToken cancellationToken)
    {
        return await _service.InsertAsync(configuration, cancellationToken);
    }
}