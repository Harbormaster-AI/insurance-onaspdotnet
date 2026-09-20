using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ExposureEndpoints
{
    public static IEndpointRouteBuilder MapExposureEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exposure").WithTags("Exposures");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);
        group.MapPut("/assignPolicyCoverage", AssignPolicyCoverage);
        group.MapPut("/unassignPolicyCoverage", UnassignPolicyCoverage);
        group.MapPut("/assignInsuredObject", AssignInsuredObject);
        group.MapPut("/unassignInsuredObject", UnassignInsuredObject);

    group.MapPut("/addToReserves", AddToReserves);
    group.MapPut("/removeFromReserves", RemoveFromReserves);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);


        return app;
    }

    private static async Task<IResult> Create(
        ExposureRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExposure( request );

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
        ExposureRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToExposure( request );

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
        IExposureService service,
        CancellationToken cancellationToken) {

        var exposure = await service.Get(identifier, cancellationToken);
        return exposure is null ? Results.NotFound() : Results.Ok( exposure );
    }


    private static async Task<IResult> GetAll(
        IExposureService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ExposureResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IExposureService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IExposureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicyCoverage(
        AssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicyCoverage(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicyCoverage(
    AssociationRequest request,
    IExposureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicyCoverage(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsuredObject(
        AssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsuredObject(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsuredObject(
    AssociationRequest request,
    IExposureService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsuredObject(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToReserves(
        MultipleAssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReserves(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReserves(
        MultipleAssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReserves(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IExposureService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Exposure mapRequestToExposure( ExposureRequest request ) {
        var model = new Exposure
        {
            Id = request.Id,
            ExposureType = request.ExposureType,
            Status = request.Status,
        };
        return model;
    }

}
