
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class BillingAccountRepository : IBillingAccountRepository
{
    private readonly ApplicationDbContext _db;

    public BillingAccountRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BillingAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.BillingAccounts
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BillingAccount>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.BillingAccounts
            .AsNoTracking()
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BillingAccount billingAccount, CancellationToken cancellationToken)
    {
        _db.BillingAccounts.Add(billingAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(BillingAccount billingAccount, CancellationToken cancellationToken)
    {
        _db.BillingAccounts.Update(billingAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BillingAccount billingAccount, CancellationToken cancellationToken)
    {
        _db.BillingAccounts.Remove(billingAccount);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id) &&
                EF.Property<Guid?>(
                    policy,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInvoicesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Invoices
            .Where(invoice =>
                request.ChildIds.Contains(invoice.Id) &&
                EF.Property<Guid?>(
                    invoice,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    invoice =>
                        EF.Property<Guid?>(
                            invoice,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payments
            .Where(payment =>
                request.ChildIds.Contains(payment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payment =>
                        EF.Property<Guid?>(
                            payment,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Payments
            .Where(payment =>
                request.ChildIds.Contains(payment.Id) &&
                EF.Property<Guid?>(
                    payment,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    payment =>
                        EF.Property<Guid?>(
                            payment,
                            "Document_Id"),
                    (Guid?)null));
    }

}
