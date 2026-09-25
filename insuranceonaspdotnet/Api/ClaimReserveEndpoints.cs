
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ClaimReserveEndpoints
{
    public static IEndpointRouteBuilder MapClaimReserveEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/claimReserve").WithTags("ClaimReserves");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);
        group.MapPut("/assignExposure", AssignExposure);
        group.MapPut("/unassignExposure", UnassignExposure);


        return app;
    }

    private static async Task<IResult> Create(
        ClaimReserveRequest request,
        IClaimReserveService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaimReserve( request );

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
        ClaimReserveRequest request,
        IClaimReserveService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaimReserve( request );

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
        IClaimReserveService service,
        CancellationToken cancellationToken) {

        var claimReserve = await service.Get(identifier, cancellationToken);
        return claimReserve is null ? Results.NotFound() : Results.Ok( claimReserve );
    }


    private static async Task<IResult> GetAll(
        IClaimReserveService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClaimReserveResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClaimReserveService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IClaimReserveService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IClaimReserveService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignExposure(
        AssociationRequest request,
        IClaimReserveService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignExposure(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignExposure(
    AssociationRequest request,
    IClaimReserveService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignExposure(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ClaimReserve mapRequestToClaimReserve( ClaimReserveRequest request ) {
        var model = new ClaimReserve
        {
            Id = request.Id,
            Amount = request.Amount,
            SetDate = request.SetDate,
            ReserveType = request.ReserveType,
            Status = request.Status,
        };
        return model;
    }

}
