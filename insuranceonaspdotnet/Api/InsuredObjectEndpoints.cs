using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class InsuredObjectEndpoints
{
    public static IEndpointRouteBuilder MapInsuredObjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insuredObject").WithTags("InsuredObjects");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);

    group.MapPut("/addToCoverages", AddToCoverages);
    group.MapPut("/removeFromCoverages", RemoveFromCoverages);


        return app;
    }

    private static async Task<IResult> Create(
        InsuredObjectRequest request,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsuredObject( request );

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
        InsuredObjectRequest request,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsuredObject( request );

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
        IInsuredObjectService service,
        CancellationToken cancellationToken) {

        var insuredObject = await service.Get(identifier, cancellationToken);
        return insuredObject is null ? Results.NotFound() : Results.Ok( insuredObject );
    }


    private static async Task<IResult> GetAll(
        IInsuredObjectService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsuredObjectResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IInsuredObjectService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCoverages(
        MultipleAssociationRequest request,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoverages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoverages(
        MultipleAssociationRequest request,
        IInsuredObjectService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoverages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InsuredObject mapRequestToInsuredObject( InsuredObjectRequest request ) {
        var model = new InsuredObject
        {
            Id = request.Id,
            Description = request.Description,
            SerialOrId = request.SerialOrId,
            PrimaryAddress = request.PrimaryAddress,
            ObjectType = request.ObjectType,
        };
        return model;
    }

}
