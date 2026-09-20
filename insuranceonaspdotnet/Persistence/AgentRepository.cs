using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class AgentRepository : IAgentRepository
{
    private readonly ApplicationDbContext _db;

    public AgentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Agent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Agents
            .Include(x => x.Distributor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Agent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Agents
            .AsNoTracking()
            .Include(x => x.Distributor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Add(agent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Update(agent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Remove(agent);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
