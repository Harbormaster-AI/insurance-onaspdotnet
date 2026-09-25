
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class AdjusterEndpoints
{
    public static IEndpointRouteBuilder MapAdjusterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/adjuster").WithTags("Adjusters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToServiceProviders", AddToServiceProviders);
    group.MapPut("/removeFromServiceProviders", RemoveFromServiceProviders);


        return app;
    }

    private static async Task<IResult> Create(
        AdjusterRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdjuster( request );

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
        AdjusterRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAdjuster( request );

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
        IAdjusterService service,
        CancellationToken cancellationToken) {

        var adjuster = await service.Get(identifier, cancellationToken);
        return adjuster is null ? Results.NotFound() : Results.Ok( adjuster );
    }


    private static async Task<IResult> GetAll(
        IAdjusterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AdjusterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAdjusterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToServiceProviders(
        MultipleAssociationRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToServiceProviders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromServiceProviders(
        MultipleAssociationRequest request,
        IAdjusterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromServiceProviders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Adjuster mapRequestToAdjuster( AdjusterRequest request ) {
        var model = new Adjuster
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseNumber = request.LicenseNumber,
            AdjusterType = request.AdjusterType,
        };
        return model;
    }

}
