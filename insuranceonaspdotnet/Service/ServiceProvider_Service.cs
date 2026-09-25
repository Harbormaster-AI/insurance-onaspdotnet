
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IServiceProvider_Service {

    Task Create(ServiceProvider_ model , CancellationToken cancellationToken);
    Task<bool> Update(ServiceProvider_ model, CancellationToken cancellationToken);
    Task<ServiceProvider_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ServiceProvider_>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ServiceProvider_Service : IServiceProvider_Service
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IServiceProvider_Repository _repository;
    private readonly ILogger<ServiceProvider_Service> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ServiceProvider_Service(
        ApplicationTelemetry telemetry,
        IServiceProvider_Repository repository,
        ILogger<ServiceProvider_Service> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ServiceProvider_ model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ServiceProvider_",
                "CreateServiceProvider_",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ServiceProvider_ model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TaxId = model.TaxId;
            existing.ProviderType = model.ProviderType;
            existing.NetworkStatus = model.NetworkStatus;

            await _telemetry.Execute(
                "ServiceProvider_",
                "UpdateServiceProvider_",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<ServiceProvider_?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ServiceProvider_>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _telemetry.Execute(
                "ServiceProvider_",
                "UpdateServiceProvider_",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ServiceProvider_",
                "AddToClaims",
                () => _repository.AddToClaimsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ServiceProvider_",
                "RemoveFromClaims",
                () => _repository.RemoveFromClaimsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
