
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class SubrogationRecoveryEndpoints
{
    public static IEndpointRouteBuilder MapSubrogationRecoveryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/subrogationRecovery").WithTags("SubrogationRecoverys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);
        group.MapPut("/assignExposure", AssignExposure);
        group.MapPut("/unassignExposure", UnassignExposure);
        group.MapPut("/assignCounterparty", AssignCounterparty);
        group.MapPut("/unassignCounterparty", UnassignCounterparty);


        return app;
    }

    private static async Task<IResult> Create(
        SubrogationRecoveryRequest request,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSubrogationRecovery( request );

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
        SubrogationRecoveryRequest request,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSubrogationRecovery( request );

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
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {

        var subrogationRecovery = await service.Get(identifier, cancellationToken);
        return subrogationRecovery is null ? Results.NotFound() : Results.Ok( subrogationRecovery );
    }


    private static async Task<IResult> GetAll(
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SubrogationRecoveryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    ISubrogationRecoveryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignExposure(
        AssociationRequest request,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignExposure(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignExposure(
    AssociationRequest request,
    ISubrogationRecoveryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignExposure(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCounterparty(
        AssociationRequest request,
        ISubrogationRecoveryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCounterparty(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCounterparty(
    AssociationRequest request,
    ISubrogationRecoveryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCounterparty(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static SubrogationRecovery mapRequestToSubrogationRecovery( SubrogationRecoveryRequest request ) {
        var model = new SubrogationRecovery
        {
            Id = request.Id,
            RecoveryReference = request.RecoveryReference,
            Amount = request.Amount,
            RecoveryDate = request.RecoveryDate,
            Status = request.Status,
        };
        return model;
    }

}
