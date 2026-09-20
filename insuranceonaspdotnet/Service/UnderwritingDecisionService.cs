using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IUnderwritingDecisionService {

    Task Create(UnderwritingDecision model , CancellationToken cancellationToken);
    Task<bool> Update(UnderwritingDecision model, CancellationToken cancellationToken);
    Task<UnderwritingDecision?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<UnderwritingDecision>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignQuote(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignQuote(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignUnderwriter(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignUnderwriter(AssociationRequest request, CancellationToken cancellationToken);


}

public class UnderwritingDecisionService : IUnderwritingDecisionService
{
    private readonly IUnderwritingDecisionRepository _repository;
    private readonly ILogger<UnderwritingDecisionService> _logger;

    public UnderwritingDecisionService(
        IUnderwritingDecisionRepository repository, ILogger<UnderwritingDecisionService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(UnderwritingDecision model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(UnderwritingDecision model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Notes = model.Notes;
            existing.DecisionDate = model.DecisionDate;
            existing.Decision = model.Decision;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<UnderwritingDecision?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<UnderwritingDecision>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignQuote(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignQuote(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignUnderwriter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignUnderwriter(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
