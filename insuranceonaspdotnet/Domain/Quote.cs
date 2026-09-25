
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Quote
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? QuoteId { get; set; } 
 public virtual string? QuoteNumber { get; set; } 
 public virtual Money? TotalPremium { get; set; } 
 public virtual DateOnly? RatingDate { get; set; } 
 public virtual bool? AsBound { get; set; } 
public virtual Application? Application { get; set; } 
public virtual ICollection<UnderwritingDecision> UnderwritingDecisions { get; set; } = new List<UnderwritingDecision>();
public virtual Policy? Policy { get; set; } 

    public static Quote FromRequest(QuoteRequest request) {
        return new Quote {
            Id = request.Id,
            QuoteNumber = request.QuoteNumber,
            TotalPremium = request.TotalPremium,
            RatingDate = request.RatingDate,
            AsBound = request.AsBound,
        };
    }
}
