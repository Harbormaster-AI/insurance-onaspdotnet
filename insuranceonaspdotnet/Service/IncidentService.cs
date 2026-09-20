using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IIncidentService {

    Task Create(Incident model , CancellationToken cancellationToken);
    Task<bool> Update(Incident model, CancellationToken cancellationToken);
    Task<Incident?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Incident>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignClaim(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignClaim(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _repository;
    private readonly ILogger<IncidentService> _logger;

    public IncidentService(
        IIncidentRepository repository, ILogger<IncidentService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Incident model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(Incident model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Location = model.Location;
            existing.Description = model.Description;
            existing.IncidentType = model.IncidentType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Incident?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Incident>> GetAll(CancellationToken cancellationToken)
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignClaim(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignClaim(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
