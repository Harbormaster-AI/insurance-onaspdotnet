using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class DistributorEndpoints
{
    public static IEndpointRouteBuilder MapDistributorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/distributor").WithTags("Distributors");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToInsurers", AddToInsurers);
    group.MapPut("/removeFromInsurers", RemoveFromInsurers);

    group.MapPut("/addToAgents", AddToAgents);
    group.MapPut("/removeFromAgents", RemoveFromAgents);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);


        return app;
    }

    private static async Task<IResult> Create(
        DistributorRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDistributor( request );

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
        DistributorRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDistributor( request );

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
        IDistributorService service,
        CancellationToken cancellationToken) {

        var distributor = await service.Get(identifier, cancellationToken);
        return distributor is null ? Results.NotFound() : Results.Ok( distributor );
    }


    private static async Task<IResult> GetAll(
        IDistributorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DistributorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInsurers(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsurers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsurers(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsurers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAgents(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAgents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAgents(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAgents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IDistributorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Distributor mapRequestToDistributor( DistributorRequest request ) {
        var model = new Distributor
        {
            Id = request.Id,
            Name = request.Name,
            LicenseNumber = request.LicenseNumber,
            Region = request.Region,
            DistributorType = request.DistributorType,
        };
        return model;
    }

}
