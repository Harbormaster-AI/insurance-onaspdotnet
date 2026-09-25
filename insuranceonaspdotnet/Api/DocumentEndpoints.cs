
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class DocumentEndpoints
{
    public static IEndpointRouteBuilder MapDocumentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/document").WithTags("Documents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);
        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);
        group.MapPut("/assignApplication", AssignApplication);
        group.MapPut("/unassignApplication", UnassignApplication);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);


        return app;
    }

    private static async Task<IResult> Create(
        DocumentRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDocument( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        DocumentRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDocument( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IDocumentService service,
        CancellationToken cancellationToken) {

        var document = await service.Get(identifier, cancellationToken);
        return document is null ? Results.NotFound() : Results.Ok( document );
    }


    private static async Task<IResult> GetAll(
        IDocumentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DocumentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApplication(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApplication(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApplication(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApplication(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Document mapRequestToDocument( DocumentRequest request ) {
        var model = new Document
        {
            Id = request.Id,
            FileName = request.FileName,
            UploadedDate = request.UploadedDate,
            DocumentType = request.DocumentType,
        };
        return model;
    }

}
