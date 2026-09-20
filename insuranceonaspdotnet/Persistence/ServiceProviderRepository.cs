using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ServiceProviderRepository : IServiceProviderRepository
{
    private readonly ApplicationDbContext _db;

    public ServiceProviderRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ServiceProviders
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ServiceProvider>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ServiceProviders
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _db.ServiceProviders.Add(serviceProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _db.ServiceProviders.Update(serviceProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        _db.ServiceProviders.Remove(serviceProvider);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
