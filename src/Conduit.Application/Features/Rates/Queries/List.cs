using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conduit.Application.Extensions;
using Conduit.Application.Features.Orders.Queries;
using Conduit.Application.Interfaces;
using Conduit.Application.Support;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Conduit.Application.Features.Rates.Queries;
public record MultipleRatesResponse(IEnumerable<RateDto> Rates, int RatesCount)
{

}

public class RatesListQuery : PagedQuery, IRequest<MultipleRatesResponse>
{
    /// <summary>
    /// Filter by id (Rate id)
    /// </summary>

    public int? Id { get; set; }
}

public class RatesListHandler : IRequestHandler<RatesListQuery, MultipleRatesResponse>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    public RatesListHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<MultipleRatesResponse> Handle(RatesListQuery request, CancellationToken cancellationToken)
    {
        var rates = await _context.Rates
            .Include(r => r.Article)
            .Include(r => r.User)
            .Select(r => r.Map(_currentUser.User))
            .PaginateAsync(request, cancellationToken);
        return new MultipleRatesResponse(rates.Items, rates.Total);
    }
}


