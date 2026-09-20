using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class SubrogationRecoveryRepository : ISubrogationRecoveryRepository
{
    private readonly ApplicationDbContext _db;

    public SubrogationRecoveryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SubrogationRecovery?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.SubrogationRecoverys
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .Include(x => x.Counterparty)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<SubrogationRecovery>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.SubrogationRecoverys
            .AsNoTracking()
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .Include(x => x.Counterparty)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken)
    {
        _db.SubrogationRecoverys.Add(subrogationRecovery);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken)
    {
        _db.SubrogationRecoverys.Update(subrogationRecovery);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken)
    {
        _db.SubrogationRecoverys.Remove(subrogationRecovery);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
