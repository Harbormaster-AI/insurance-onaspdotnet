using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public ApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Application?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Applications
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.Distributor)
            .Include(x => x.SelectedQuote)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Application>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Applications
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.Distributor)
            .Include(x => x.SelectedQuote)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Application application, CancellationToken cancellationToken)
    {
        _db.Applications.Add(application);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Application application, CancellationToken cancellationToken)
    {
        _db.Applications.Update(application);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Application application, CancellationToken cancellationToken)
    {
        _db.Applications.Remove(application);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
