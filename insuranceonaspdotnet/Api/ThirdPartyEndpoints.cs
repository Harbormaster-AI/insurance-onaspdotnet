using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ThirdPartyEndpoints
{
    public static IEndpointRouteBuilder MapThirdPartyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/thirdParty").WithTags("ThirdPartys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToSubrogations", AddToSubrogations);
    group.MapPut("/removeFromSubrogations", RemoveFromSubrogations);


        return app;
    }

    private static async Task<IResult> Create(
        ThirdPartyRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdParty( request );

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
        ThirdPartyRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToThirdParty( request );

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
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var thirdParty = await service.Get(identifier, cancellationToken);
        return thirdParty is null ? Results.NotFound() : Results.Ok( thirdParty );
    }


    private static async Task<IResult> GetAll(
        IThirdPartyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ThirdPartyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSubrogations(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubrogations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubrogations(
        MultipleAssociationRequest request,
        IThirdPartyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubrogations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ThirdParty mapRequestToThirdParty( ThirdPartyRequest request ) {
        var model = new ThirdParty
        {
            Id = request.Id,
            Name = request.Name,
            TaxId = request.TaxId,
            Address = request.Address,
            PartyType = request.PartyType,
        };
        return model;
    }

}
