using Conduit.Application.Features.Articles.Queries;
using Conduit.Application.Features.Orders.Queries;
using Conduit.Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conduit.Presentation.Controllers;

[Route("[controller]")]
[ApiExplorerSettings(GroupName = "Orders")]
[Authorize]
public class OrdersController
{
    private readonly ISender _sender;
    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Get recent orders globally
    /// </summary>
    /// <remarks>Get all orders globally. </remarks>
    /// <returns></returns>
    [HttpGet(Name = "GetOrders")]
    [AllowAnonymous]
    public Task<MultipleOrdersResponse> List([FromQuery] OrdersListQuery query, CancellationToken cancellationToken)
    {

        // insert GET logic here
        // should return something similar as the GET endpoint in the other *Controller.cs files - like Task<MutlipleOrderResponse>
        return _sender.Send(query, cancellationToken);
    }
}
