using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

public class ConfigurationsController : BaseApiController
{
    private readonly IConfigurationService _service;

    public ConfigurationsController(IConfigurationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ConfigurationSelectDto> InsertAsync(ConfigurationInsertDto configuration,
        CancellationToken cancellationToken)
    {
        return await _service.InsertAsync(configuration, cancellationToken);
    }
}