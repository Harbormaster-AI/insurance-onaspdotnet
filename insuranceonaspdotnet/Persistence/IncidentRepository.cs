using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class IncidentRepository : IIncidentRepository
{
    private readonly ApplicationDbContext _db;

    public IncidentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Incidents
            .Include(x => x.Claim)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Incident>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Incidents
            .AsNoTracking()
            .Include(x => x.Claim)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Incident incident, CancellationToken cancellationToken)
    {
        _db.Incidents.Add(incident);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Incident incident, CancellationToken cancellationToken)
    {
        _db.Incidents.Update(incident);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Incident incident, CancellationToken cancellationToken)
    {
        _db.Incidents.Remove(incident);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
