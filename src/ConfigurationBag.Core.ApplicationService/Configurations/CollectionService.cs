using AutoMapper;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Common.Services;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConfigurationBag.Core.ApplicationService.Configurations;

public interface ICollectionService : IService
{
    Task<CollectionSelectDto> InsertAsync(CollectionInsertDto app, CancellationToken cancellationToken);
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

    public async Task<CollectionSelectDto> InsertAsync(CollectionInsertDto app, CancellationToken cancellationToken)
    {
        var entity = app.ToEntity(_mapper);
        await _repository.AddAsync(entity, cancellationToken);
        return CollectionSelectDto.FromEntity(_mapper, entity);
    }
}