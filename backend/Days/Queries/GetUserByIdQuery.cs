using FluentValidation;
using Infrastructure;
using Infrastructure.Entities;
using MediatR;

namespace Days.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public long Id { get; init; }
}

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator(DaysContext context)
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(async (id, cancellation) =>
                await context.Users.FindAsync(new object?[] { id }, cancellation) != null)
            .WithMessage("Id is invalid.");
    }
}

public class GetUserByIdQueryHandler(DaysContext context) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return (await context.Users.FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken))!;
    }
}