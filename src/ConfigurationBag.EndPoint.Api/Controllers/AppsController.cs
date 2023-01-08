using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

public class AppsController : BaseApiController
{
    private readonly IAppService _service;

    public AppsController(IAppService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<AppSelectDto> InsertAsync(AppInsertDto app,
        CancellationToken cancellationToken)
    {
        return await _service.InsertAsync(app, cancellationToken);
    }
}