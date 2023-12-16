using FluentValidation;
using Infrastructure.Entities;
using MediatR;
using AppContext = Infrastructure.AppContext;

namespace API.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public long Id { get; init; }
}

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator(AppContext context)
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .NotNull()
            .MustAsync(async (id, cancellation) =>
                await context.Users.FindAsync(new object?[] { id }, cancellation) != null)
            .WithMessage("Id is invalid.");
    }
}

public class GetUserByIdQueryHandler(AppContext context) : IRequestHandler<GetUserByIdQuery, User>
{
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return (await context.Users.FindAsync(new object?[] { request.Id, cancellationToken }, cancellationToken))!;
    }
}