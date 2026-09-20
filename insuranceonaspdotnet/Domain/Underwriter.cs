using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Underwriter
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? UnderwriterId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? EmployeeId { get; set; } 
 public virtual Money? AuthorityLimit { get; set; } 
public virtual ICollection<UnderwritingDecision> Decisions { get; set; } = new List<UnderwritingDecision>();
public virtual Insurer? Insurer { get; set; } 

    public static Underwriter FromRequest(UnderwriterRequest request) {
        return new Underwriter {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmployeeId = request.EmployeeId,
            AuthorityLimit = request.AuthorityLimit,
        };
    }
}
