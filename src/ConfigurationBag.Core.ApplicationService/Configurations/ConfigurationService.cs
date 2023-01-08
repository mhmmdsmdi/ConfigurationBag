using AutoMapper;
using AutoMapper.QueryableExtensions;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Common.Services;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConfigurationBag.Core.ApplicationService.Configurations;

public interface IConfigurationService : IService
{
    /// <summary>
    /// Get all configurations
    /// </summary>
    /// <param name="collectionId">Collection id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ICollection<ConfigurationSelectDto>> Get(long collectionId, CancellationToken cancellationToken);

    /// <summary>
    /// Create configuration
    /// </summary>
    /// <param name="configuration">Configuration</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ConfigurationSelectDto> InsertAsync(ConfigurationInsertDto configuration, CancellationToken cancellationToken);
}

public class ConfigurationService : IConfigurationService
{
    private readonly ILogger<ConfigurationService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Configuration> _repository;

    public ConfigurationService(ILogger<ConfigurationService> logger, IMapper mapper, IRepository<Configuration> repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Get all configurations
    /// </summary>
    /// <param name="collectionId">Collection id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ICollection<ConfigurationSelectDto>> Get(long collectionId, CancellationToken cancellationToken)
    {
        return await _repository.TableNoTracking
            .Where(x => x.CollectionId == collectionId)
            .ProjectTo<ConfigurationSelectDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Create configuration
    /// </summary>
    /// <param name="configuration">Configuration</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ConfigurationSelectDto> InsertAsync(ConfigurationInsertDto configuration, CancellationToken cancellationToken)
    {
        var entity = configuration.ToEntity(_mapper);
        await _repository.AddAsync(entity, cancellationToken);
        return ConfigurationSelectDto.FromEntity(_mapper, entity);
    }
}