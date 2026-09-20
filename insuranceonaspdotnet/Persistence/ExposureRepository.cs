using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ExposureRepository : IExposureRepository
{
    private readonly ApplicationDbContext _db;

    public ExposureRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Exposure?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Exposures
            .Include(x => x.Claim)
            .Include(x => x.PolicyCoverage)
            .Include(x => x.InsuredObject)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Exposure>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Exposures
            .AsNoTracking()
            .Include(x => x.Claim)
            .Include(x => x.PolicyCoverage)
            .Include(x => x.InsuredObject)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Add(exposure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Update(exposure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Remove(exposure);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
