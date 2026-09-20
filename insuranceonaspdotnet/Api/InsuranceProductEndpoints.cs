using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class InsuranceProductEndpoints
{
    public static IEndpointRouteBuilder MapInsuranceProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insuranceProduct").WithTags("InsuranceProducts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInsurer", AssignInsurer);
        group.MapPut("/unassignInsurer", UnassignInsurer);

    group.MapPut("/addToCoverageDefinitions", AddToCoverageDefinitions);
    group.MapPut("/removeFromCoverageDefinitions", RemoveFromCoverageDefinitions);


        return app;
    }

    private static async Task<IResult> Create(
        InsuranceProductRequest request,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsuranceProduct( request );

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
        InsuranceProductRequest request,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsuranceProduct( request );

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
        IInsuranceProductService service,
        CancellationToken cancellationToken) {

        var insuranceProduct = await service.Get(identifier, cancellationToken);
        return insuranceProduct is null ? Results.NotFound() : Results.Ok( insuranceProduct );
    }


    private static async Task<IResult> GetAll(
        IInsuranceProductService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsuranceProductResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsurer(
        AssociationRequest request,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsurer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsurer(
    AssociationRequest request,
    IInsuranceProductService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsurer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCoverageDefinitions(
        MultipleAssociationRequest request,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoverageDefinitions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoverageDefinitions(
        MultipleAssociationRequest request,
        IInsuranceProductService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoverageDefinitions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InsuranceProduct mapRequestToInsuranceProduct( InsuranceProductRequest request ) {
        var model = new InsuranceProduct
        {
            Id = request.Id,
            Name = request.Name,
            ProductCode = request.ProductCode,
            LineOfBusiness = request.LineOfBusiness,
        };
        return model;
    }

}
