
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? IncidentId { get; set; } 
 public virtual Address? Location { get; set; } 
 public virtual string? Description { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual ICollection<InsuredObject> InsuredObjects { get; set; } = new List<InsuredObject>();
 public virtual PerilType? IncidentType { get; set; } 

    public static Incident FromRequest(IncidentRequest request) {
        return new Incident {
            Id = request.Id,
            Location = request.Location,
            Description = request.Description,
            IncidentType = request.IncidentType,
        };
    }
}
