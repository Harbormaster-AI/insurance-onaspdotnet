
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IDistributorService {

    Task Create(Distributor model , CancellationToken cancellationToken);
    Task<bool> Update(Distributor model, CancellationToken cancellationToken);
    Task<Distributor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Distributor>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToInsurers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInsurers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAgents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAgents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class DistributorService : IDistributorService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IDistributorRepository _repository;
    private readonly ILogger<DistributorService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public DistributorService(
        ApplicationTelemetry telemetry,
        IDistributorRepository repository,
        ILogger<DistributorService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Distributor model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Distributor",
                "CreateDistributor",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Distributor model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LicenseNumber = model.LicenseNumber;
            existing.Region = model.Region;
            existing.DistributorType = model.DistributorType;

            await _telemetry.Execute(
                "Distributor",
                "UpdateDistributor",
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

    public Task<Distributor?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Distributor>> GetAll(CancellationToken cancellationToken)
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
                "Distributor",
                "UpdateDistributor",
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


    public async Task<bool> AddToInsurers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "AddToInsurers",
                () => _repository.AddToInsurersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInsurers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "RemoveFromInsurers",
                () => _repository.RemoveFromInsurersAsync(request, cancellationToken));
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

    public async Task<bool> AddToAgents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "AddToAgents",
                () => _repository.AddToAgentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAgents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "RemoveFromAgents",
                () => _repository.RemoveFromAgentsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "AddToPolicies",
                () => _repository.AddToPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Distributor",
                "RemoveFromPolicies",
                () => _repository.RemoveFromPoliciesAsync(request, cancellationToken));
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
