
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class EndorsementEndpoints
{
    public static IEndpointRouteBuilder MapEndorsementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/endorsement").WithTags("Endorsements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        EndorsementRequest request,
        IEndorsementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEndorsement( request );

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
        EndorsementRequest request,
        IEndorsementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToEndorsement( request );

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
        IEndorsementService service,
        CancellationToken cancellationToken) {

        var endorsement = await service.Get(identifier, cancellationToken);
        return endorsement is null ? Results.NotFound() : Results.Ok( endorsement );
    }


    private static async Task<IResult> GetAll(
        IEndorsementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( EndorsementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IEndorsementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IEndorsementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IEndorsementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Endorsement mapRequestToEndorsement( EndorsementRequest request ) {
        var model = new Endorsement
        {
            Id = request.Id,
            EndorsementNumber = request.EndorsementNumber,
            EffectiveDate = request.EffectiveDate,
            Description = request.Description,
        };
        return model;
    }

}
