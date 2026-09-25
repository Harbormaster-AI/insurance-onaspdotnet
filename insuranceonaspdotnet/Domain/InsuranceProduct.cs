
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class InsuranceProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsuranceproductId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? ProductCode { get; set; } 
public virtual Insurer? Insurer { get; set; } 
public virtual ICollection<CoverageDefinition> CoverageDefinitions { get; set; } = new List<CoverageDefinition>();
 public virtual LineOfBusiness? LineOfBusiness { get; set; } 

    public static InsuranceProduct FromRequest(InsuranceProductRequest request) {
        return new InsuranceProduct {
            Id = request.Id,
            Name = request.Name,
            ProductCode = request.ProductCode,
            LineOfBusiness = request.LineOfBusiness,
        };
    }
}
