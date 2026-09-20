using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class InsuranceProductRepository : IInsuranceProductRepository
{
    private readonly ApplicationDbContext _db;

    public InsuranceProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InsuranceProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InsuranceProducts
            .Include(x => x.Insurer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InsuranceProduct>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InsuranceProducts
            .AsNoTracking()
            .Include(x => x.Insurer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken)
    {
        _db.InsuranceProducts.Add(insuranceProduct);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken)
    {
        _db.InsuranceProducts.Update(insuranceProduct);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken)
    {
        _db.InsuranceProducts.Remove(insuranceProduct);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
