using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conduit.Application.Extensions;
using Conduit.Application.Features.Articles.Queries;
using Conduit.Application.Interfaces;
using Conduit.Application.Support;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Conduit.Application.Features.Orders.Queries;
public record MultipleOrdersResponse(IEnumerable<OrderDto> Orders, int OrdersCount)
{
    
}

public class OrdersListQuery: PagedQuery, IRequest<MultipleOrdersResponse>
{
    /// <summary>
    /// Filter by id (Order id)
    /// </summary>

    public int? Id { get; set; }
}

public class OrdersListHandler : IRequestHandler<OrdersListQuery, MultipleOrdersResponse>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    public OrdersListHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<MultipleOrdersResponse> Handle(OrdersListQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Include(o => o.Article)
            .Include(o => o.User)
            .Select(o => o.Map(_currentUser.User))
            .PaginateAsync(request, cancellationToken);
            return new MultipleOrdersResponse(orders.Items, orders.Total);
    }
}
