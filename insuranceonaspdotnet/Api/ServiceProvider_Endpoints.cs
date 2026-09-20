using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ServiceProvider_Endpoints
{
    public static IEndpointRouteBuilder MapServiceProvider_Endpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/serviceProvider_").WithTags("ServiceProvider_s");

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
        ServiceProvider_Request request,
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceProvider_( request );

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
        ServiceProvider_Request request,
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {

        var model = mapRequestToServiceProvider_( request );

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
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {

        var serviceProvider_ = await service.Get(identifier, cancellationToken);
        return serviceProvider_ is null ? Results.NotFound() : Results.Ok( serviceProvider_ );
    }


    private static async Task<IResult> GetAll(
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ServiceProvider_Response.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IServiceProvider_Service service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ServiceProvider_ mapRequestToServiceProvider_( ServiceProvider_Request request ) {
        var model = new ServiceProvider_
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
