
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _db;

    public PaymentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Payments
            .Include(x => x.Invoice)
            .Include(x => x.BillingAccount)
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Payments
            .AsNoTracking()
            .Include(x => x.Invoice)
            .Include(x => x.BillingAccount)
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Add(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Update(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Payment payment, CancellationToken cancellationToken)
    {
        _db.Payments.Remove(payment);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
