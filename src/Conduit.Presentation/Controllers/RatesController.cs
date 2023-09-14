
using Conduit.Application.Features.Rates.Commands;
using Conduit.Application.Features.Rates.Queries;
using Conduit.Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Presentation.Controllers;

[Route("[controller]")]
[ApiExplorerSettings(GroupName = "Rates")]
[Authorize]
public class RatesController
{
    private readonly ISender _sender;
    public RatesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(Name = "GetRates")]
    [AllowAnonymous]
    public Task<MultipleRatesResponse> List([FromQuery] RatesListQuery query, CancellationToken cancellationToken)
    {
        return _sender.Send(query, cancellationToken);
    }

    [HttpPost(Name = "CreateArticleRate")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
    public Task<SingleRateResponse> Create(string slug, [FromBody] NewRateRequest request, CancellationToken cancellationToken)
    {
        return _sender.Send(new NewRateCommand(slug, request.Rate), cancellationToken);
    }
}

public record NewRateRequest(NewRateDto Rate);


