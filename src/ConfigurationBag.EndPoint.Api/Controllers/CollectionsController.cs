using ConfigurationBag.Core.ApplicationService.Configurations;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Controllers;

/// <summary>
/// Collections
/// </summary>
public class CollectionsController : BaseApiController
{
    private readonly ICollectionService _service;
    private readonly IConfigurationService _configurationService;

    public CollectionsController(ICollectionService service, IConfigurationService configurationService)
    {
        _service = service;
        _configurationService = configurationService;
    }

    /// <summary>
    /// Get all collections
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ICollection<CollectionSelectDto>> Get(CancellationToken cancellationToken)
    {
        return await _service.Get(cancellationToken);
    }

    /// <summary>
    /// Create collection
    /// </summary>
    /// <param name="collection">Collection</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<CollectionSelectDto> InsertAsync(CollectionInsertDto collection,
        CancellationToken cancellationToken)
    {
        return await _service.InsertAsync(collection, cancellationToken);
    }

    /// <summary>
    /// Get all configurations
    /// </summary>
    /// <param name="collectionId">Collection Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{collectionId}/Configurations")]
    public async Task<ICollection<ConfigurationSelectDto>> Get(long collectionId, CancellationToken cancellationToken)
    {
        return await _configurationService.Get(collectionId, cancellationToken);
    }
}