using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class QuoteRepository : IQuoteRepository
{
    private readonly ApplicationDbContext _db;

    public QuoteRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Quote?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Quotes
            .Include(x => x.Application)
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Quote>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Quotes
            .AsNoTracking()
            .Include(x => x.Application)
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Add(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Update(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Quote quote, CancellationToken cancellationToken)
    {
        _db.Quotes.Remove(quote);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
