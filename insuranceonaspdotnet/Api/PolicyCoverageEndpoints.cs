using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class PolicyCoverageEndpoints
{
    public static IEndpointRouteBuilder MapPolicyCoverageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/policyCoverage").WithTags("PolicyCoverages");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);

    group.MapPut("/addToInsuredObjects", AddToInsuredObjects);
    group.MapPut("/removeFromInsuredObjects", RemoveFromInsuredObjects);


        return app;
    }

    private static async Task<IResult> Create(
        PolicyCoverageRequest request,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicyCoverage( request );

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
        PolicyCoverageRequest request,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicyCoverage( request );

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
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {

        var policyCoverage = await service.Get(identifier, cancellationToken);
        return policyCoverage is null ? Results.NotFound() : Results.Ok( policyCoverage );
    }


    private static async Task<IResult> GetAll(
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PolicyCoverageResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IPolicyCoverageService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInsuredObjects(
        MultipleAssociationRequest request,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsuredObjects(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsuredObjects(
        MultipleAssociationRequest request,
        IPolicyCoverageService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsuredObjects(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PolicyCoverage mapRequestToPolicyCoverage( PolicyCoverageRequest request ) {
        var model = new PolicyCoverage
        {
            Id = request.Id,
            Limit = request.Limit,
            Deductible = request.Deductible,
            Premium = request.Premium,
            CoverageType = request.CoverageType,
        };
        return model;
    }

}
