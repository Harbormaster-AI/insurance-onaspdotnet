using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class PolicyCoverage
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PolicycoverageId { get; set; } 
 public virtual Money? Limit { get; set; } 
 public virtual Money? Deductible { get; set; } 
 public virtual Money? Premium { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ICollection<InsuredObject> InsuredObjects { get; set; } = new List<InsuredObject>();
 public virtual CoverageType? CoverageType { get; set; } 

    public static PolicyCoverage FromRequest(PolicyCoverageRequest request) {
        return new PolicyCoverage {
            Id = request.Id,
            Limit = request.Limit,
            Deductible = request.Deductible,
            Premium = request.Premium,
            CoverageType = request.CoverageType,
        };
    }
}
