using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ServiceProviderEndpoints
{
    public static IEndpointRouteBuilder MapServiceProviderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/serviceProvider").WithTags("ServiceProviders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);


        return app;
    }

    private static async Task<IResult> Create(
        ServiceProviderRequest request,
        IServiceProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceProvider( request );

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
        ServiceProviderRequest request,
        IServiceProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceProvider( request );

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
        IServiceProviderService service,
        CancellationToken cancellationToken) {

        var serviceProvider = await service.Get(identifier, cancellationToken);
        return serviceProvider is null ? Results.NotFound() : Results.Ok( serviceProvider );
    }


    private static async Task<IResult> GetAll(
        IServiceProviderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ServiceProviderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IServiceProviderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IServiceProviderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IServiceProviderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ServiceProvider mapRequestToServiceProvider( ServiceProviderRequest request ) {
        var model = new ServiceProvider
        {
            Id = request.Id,
            Name = request.Name,
            TaxId = request.TaxId,
            ProviderType = request.ProviderType,
            NetworkStatus = request.NetworkStatus,
        };
        return model;
    }

}
