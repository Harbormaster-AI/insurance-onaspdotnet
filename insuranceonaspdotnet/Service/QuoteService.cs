using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IQuoteService {

    Task Create(Quote model , CancellationToken cancellationToken);
    Task<bool> Update(Quote model, CancellationToken cancellationToken);
    Task<Quote?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Quote>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignApplication(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignApplication(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToUnderwritingDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromUnderwritingDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _repository;
    private readonly ILogger<QuoteService> _logger;

    public QuoteService(
        IQuoteRepository repository, ILogger<QuoteService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Quote model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Quote model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.QuoteNumber = model.QuoteNumber;
            existing.TotalPremium = model.TotalPremium;
            existing.RatingDate = model.RatingDate;
            existing.AsBound = model.AsBound;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Quote?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Quote>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignApplication(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignApplication(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToUnderwritingDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromUnderwritingDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
