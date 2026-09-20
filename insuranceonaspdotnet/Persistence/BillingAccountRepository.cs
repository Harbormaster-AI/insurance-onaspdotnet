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
}
