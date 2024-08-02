using Api.Routes.Requests;
using Api.Routes.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes;

[ApiController]
[Route("v1/routes")]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator mediator;

    public RoutesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SearchResponseV1), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRoutes([FromBody] SearchRequestV1 request, CancellationToken token)
    {
        SearchResponseV1? response = default;
        
        if (request.OnlyCached.HasValue && request.OnlyCached.Value)
        {
            var query = request.MapToGetCachedByFilterQuery();
            var result = await mediator.Send(query, token);
            response = result.MapToSearchResponseV1();
        }
        else
        {
            var query = request.MapToGetByFilterQuery();
            var result = await mediator.Send(query, token);
            response = result.MapToSearchResponseV1();
        }

        return Ok(response);
    }
}