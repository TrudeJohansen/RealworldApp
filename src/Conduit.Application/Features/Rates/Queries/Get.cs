using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Conduit.Application.Features.Articles.Queries;
using Conduit.Application.Features.Rates.Queries;
using Conduit.Domain.Entities;

namespace Conduit.Application.Features.Rates.Queries;
public class RateDto
{
    public int Id { get; set; }
    public int Rateing { get; set; }
}

public record SingleRateResponse(RateDto Rate);
public static class RateMapper
{
    public static RateDto Map(this Rate rate, User? currentUser)
    {
        return new()
        {
            Id = rate.Id,
            Rateing = rate.Rateing,
        };
    }
}

