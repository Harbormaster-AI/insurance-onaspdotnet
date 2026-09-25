
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class UnderwritingDecision
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? UnderwritingdecisionId { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual DateOnly? DecisionDate { get; set; } 
public virtual Quote? Quote { get; set; } 
public virtual Underwriter? Underwriter { get; set; } 
 public virtual UnderwritingDecisionType? Decision { get; set; } 

    public static UnderwritingDecision FromRequest(UnderwritingDecisionRequest request) {
        return new UnderwritingDecision {
            Id = request.Id,
            Notes = request.Notes,
            DecisionDate = request.DecisionDate,
            Decision = request.Decision,
        };
    }
}
