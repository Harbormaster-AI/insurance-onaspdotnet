using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class CoverageDefinitionRepository : ICoverageDefinitionRepository
{
    private readonly ApplicationDbContext _db;

    public CoverageDefinitionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<CoverageDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.CoverageDefinitions
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<CoverageDefinition>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.CoverageDefinitions
            .AsNoTracking()
            .Include(x => x.Product)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken)
    {
        _db.CoverageDefinitions.Add(coverageDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken)
    {
        _db.CoverageDefinitions.Update(coverageDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken)
    {
        _db.CoverageDefinitions.Remove(coverageDefinition);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
