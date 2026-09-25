
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IInsurerService {

    Task Create(Insurer model , CancellationToken cancellationToken);
    Task<bool> Update(Insurer model, CancellationToken cancellationToken);
    Task<Insurer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Insurer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToProducts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProducts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToDistributionPartners(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDistributionPartners(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsurerService : IInsurerService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInsurerRepository _repository;
    private readonly ILogger<InsurerService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InsurerService(
        ApplicationTelemetry telemetry,
        IInsurerRepository repository,
        ILogger<InsurerService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Insurer model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Insurer",
                "CreateInsurer",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Insurer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.DomicileCountry = model.DomicileCountry;
            existing.NaicNumber = model.NaicNumber;
            existing.Website = model.Website;

            await _telemetry.Execute(
                "Insurer",
                "UpdateInsurer",
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

    public Task<Insurer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Insurer>> GetAll(CancellationToken cancellationToken)
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
                "Insurer",
                "UpdateInsurer",
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


    public async Task<bool> AddToProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "AddToProducts",
                () => _repository.AddToProductsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromProducts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "RemoveFromProducts",
                () => _repository.RemoveFromProductsAsync(request, cancellationToken));
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

    public async Task<bool> AddToDistributionPartners(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "AddToDistributionPartners",
                () => _repository.AddToDistributionPartnersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDistributionPartners(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "RemoveFromDistributionPartners",
                () => _repository.RemoveFromDistributionPartnersAsync(request, cancellationToken));
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
                "Insurer",
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
                "Insurer",
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

    public async Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
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
                "Insurer",
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

    public async Task<bool> AddToReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "AddToReinsuranceAgreements",
                () => _repository.AddToReinsuranceAgreementsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Insurer",
                "RemoveFromReinsuranceAgreements",
                () => _repository.RemoveFromReinsuranceAgreementsAsync(request, cancellationToken));
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
