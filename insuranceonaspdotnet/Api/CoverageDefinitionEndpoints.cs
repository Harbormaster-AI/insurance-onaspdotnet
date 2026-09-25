
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class CoverageDefinitionEndpoints
{
    public static IEndpointRouteBuilder MapCoverageDefinitionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/coverageDefinition").WithTags("CoverageDefinitions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProduct", AssignProduct);
        group.MapPut("/unassignProduct", UnassignProduct);


        return app;
    }

    private static async Task<IResult> Create(
        CoverageDefinitionRequest request,
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoverageDefinition( request );

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
        CoverageDefinitionRequest request,
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoverageDefinition( request );

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
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {

        var coverageDefinition = await service.Get(identifier, cancellationToken);
        return coverageDefinition is null ? Results.NotFound() : Results.Ok( coverageDefinition );
    }


    private static async Task<IResult> GetAll(
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CoverageDefinitionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProduct(
        AssociationRequest request,
        ICoverageDefinitionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProduct(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProduct(
    AssociationRequest request,
    ICoverageDefinitionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProduct(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CoverageDefinition mapRequestToCoverageDefinition( CoverageDefinitionRequest request ) {
        var model = new CoverageDefinition
        {
            Id = request.Id,
            Name = request.Name,
            DefaultLimit = request.DefaultLimit,
            DefaultDeductible = request.DefaultDeductible,
            AsMandatory = request.AsMandatory,
            CoverageType = request.CoverageType,
        };
        return model;
    }

}
