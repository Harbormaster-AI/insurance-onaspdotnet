using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Adjuster
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AdjusterId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<ServiceProvider_> ServiceProviders { get; set; } = new List<ServiceProvider_>();
 public virtual AdjusterType? AdjusterType { get; set; } 

    public static Adjuster FromRequest(AdjusterRequest request) {
        return new Adjuster {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseNumber = request.LicenseNumber,
            AdjusterType = request.AdjusterType,
        };
    }
}
