using AutoMapper;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConfigurationBag.Core.ApplicationService.Configurations;

public interface IConfigurationService
{
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

    public async Task<ConfigurationSelectDto> InsertAsync(ConfigurationInsertDto configuration, CancellationToken cancellationToken)
    {
        var entity = configuration.ToEntity(_mapper);
        await _repository.AddAsync(entity, cancellationToken);
        return ConfigurationSelectDto.FromEntity(_mapper, entity);
    }
}