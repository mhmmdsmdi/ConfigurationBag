using AutoMapper;
using ConfigurationBag.Core.Common.Repositories;
using ConfigurationBag.Core.Common.Services;
using ConfigurationBag.Core.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConfigurationBag.Core.ApplicationService.Configurations;

public interface IAppService : IService
{
    Task<AppSelectDto> InsertAsync(AppInsertDto app, CancellationToken cancellationToken);
}

public class AppService : IAppService
{
    private readonly ILogger<AppService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<App> _repository;

    public AppService(ILogger<AppService> logger, IMapper mapper, IRepository<App> repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<AppSelectDto> InsertAsync(AppInsertDto app, CancellationToken cancellationToken)
    {
        var entity = app.ToEntity(_mapper);
        await _repository.AddAsync(entity, cancellationToken);
        return AppSelectDto.FromEntity(_mapper, entity);
    }
}