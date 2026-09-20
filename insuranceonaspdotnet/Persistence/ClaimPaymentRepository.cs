using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ClaimPaymentRepository : IClaimPaymentRepository
{
    private readonly ApplicationDbContext _db;

    public ClaimPaymentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ClaimPayment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ClaimPayments
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .Include(x => x.Beneficiary)
            .Include(x => x.ServiceProvider)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ClaimPayment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ClaimPayments
            .AsNoTracking()
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .Include(x => x.Beneficiary)
            .Include(x => x.ServiceProvider)
            .Include(x => x.Customer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ClaimPayment claimPayment, CancellationToken cancellationToken)
    {
        _db.ClaimPayments.Add(claimPayment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ClaimPayment claimPayment, CancellationToken cancellationToken)
    {
        _db.ClaimPayments.Update(claimPayment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ClaimPayment claimPayment, CancellationToken cancellationToken)
    {
        _db.ClaimPayments.Remove(claimPayment);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
