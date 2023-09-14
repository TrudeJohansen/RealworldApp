using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conduit.Application.Features.Articles.Queries;
using Conduit.Application.Interfaces;
using Conduit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using Conduit.Application.Features.Rates.Queries;
using Conduit.Application.Features.Comments.Queries;
using Conduit.Application.Extensions;



namespace Conduit.Application.Features.Rates.Commands;
public class NewRateDto 
{
    public int Rateing { get; set; } 

}

public record NewRateCommand(string Slug, NewRateDto Rate) : IRequest<SingleRateResponse>;


public class RateCreateValidator : AbstractValidator<NewRateCommand>
{
    public RateCreateValidator()
    {
        RuleFor(x => x.Rate.Rateing).NotNull().NotEmpty();
    }
}

public class RateCreateHandler : IRequestHandler<NewRateCommand, SingleRateResponse>
{
    private readonly IAppDbContext _context;
    private readonly ICurrentUser _currentUser;

    public RateCreateHandler(IAppDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<SingleRateResponse> Handle(NewRateCommand request, CancellationToken cancellationToken)
    {
        var article = await _context.Articles.FindAsync(x => x.Slug == request.Slug, cancellationToken);

        var rate = new Rate
        {
            Rateing = request.Rate.Rateing,
            Article = article,
            User = _currentUser.User!
        };

        await _context.Rates.AddAsync(rate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new SingleRateResponse(rate.Map(_currentUser.User));
    }
}


