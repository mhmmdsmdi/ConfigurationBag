using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Common.Services;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConfigurationBag.Core.ApplicationService.Configurations;

public interface ICollectionService : IService
{
    /// <summary>
    /// Get all collections
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<CollectionSelectDto>> Get(CancellationToken cancellationToken);

    /// <summary>
    /// Create collection
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CollectionSelectDto> InsertAsync(CollectionInsertDto collection, CancellationToken cancellationToken);
}

public class CollectionService : ICollectionService
{
    private readonly ILogger<CollectionService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Collection> _repository;

    public CollectionService(ILogger<CollectionService> logger, IMapper mapper, IRepository<Collection> repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Get all collections
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<CollectionSelectDto>> Get(CancellationToken cancellationToken)
    {
        return await _repository.TableNoTracking.ProjectTo<CollectionSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Create collection
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CollectionSelectDto> InsertAsync(CollectionInsertDto collection, CancellationToken cancellationToken)
    {
        var entity = collection.ToEntity(_mapper);
        await _repository.AddAsync(entity, cancellationToken);
        return CollectionSelectDto.FromEntity(_mapper, entity);
    }
}