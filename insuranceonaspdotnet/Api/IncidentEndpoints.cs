using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class IncidentEndpoints
{
    public static IEndpointRouteBuilder MapIncidentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/incident").WithTags("Incidents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);

    group.MapPut("/addToInsuredObjects", AddToInsuredObjects);
    group.MapPut("/removeFromInsuredObjects", RemoveFromInsuredObjects);


        return app;
    }

    private static async Task<IResult> Create(
        IncidentRequest request,
        IIncidentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToIncident( request );

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
        IncidentRequest request,
        IIncidentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToIncident( request );

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
        IIncidentService service,
        CancellationToken cancellationToken) {

        var incident = await service.Get(identifier, cancellationToken);
        return incident is null ? Results.NotFound() : Results.Ok( incident );
    }


    private static async Task<IResult> GetAll(
        IIncidentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( IncidentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IIncidentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IIncidentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IIncidentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInsuredObjects(
        MultipleAssociationRequest request,
        IIncidentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsuredObjects(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsuredObjects(
        MultipleAssociationRequest request,
        IIncidentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsuredObjects(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Incident mapRequestToIncident( IncidentRequest request ) {
        var model = new Incident
        {
            Id = request.Id,
            Location = request.Location,
            Description = request.Description,
            IncidentType = request.IncidentType,
        };
        return model;
    }

}
