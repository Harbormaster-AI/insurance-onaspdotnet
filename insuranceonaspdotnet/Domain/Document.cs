
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Document
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? DocumentId { get; set; } 
 public virtual string? FileName { get; set; } 
 public virtual DateOnly? UploadedDate { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual Application? Application { get; set; } 
public virtual Customer? Customer { get; set; } 
 public virtual DocumentType? DocumentType { get; set; } 

    public static Document FromRequest(DocumentRequest request) {
        return new Document {
            Id = request.Id,
            FileName = request.FileName,
            UploadedDate = request.UploadedDate,
            DocumentType = request.DocumentType,
        };
    }
}
