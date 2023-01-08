using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

public class CollectionsController : BaseApiController
{
    private readonly ICollectionService _service;

    public CollectionsController(ICollectionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<CollectionSelectDto> InsertAsync(CollectionInsertDto collection,
        CancellationToken cancellationToken)
    {
        return await _service.InsertAsync(collection, cancellationToken);
    }
}