using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conduit.Application.Features.Orders.Queries;
using Conduit.Application.Features.Rates.Queries;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}
