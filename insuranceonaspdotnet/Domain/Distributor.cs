using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Distributor
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DistributorId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
 public virtual string? Region { get; set; } 
public virtual ICollection<Insurer> Insurers { get; set; } = new List<Insurer>();
public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
 public virtual DistributionChannelType? DistributorType { get; set; } 

    public static Distributor FromRequest(DistributorRequest request) {
        return new Distributor {
            Id = request.Id,
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            Region = request.Region,
            DistributorType = request.DistributorType,
        };
    }
}
