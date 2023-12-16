using FluentValidation;
using MediatR;

namespace API.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationFailures = validators
            .Select(x => x.ValidateAsync(context, cancellationToken))
            .Select(t => t.Result)
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .ToList();

        if (validationFailures.Any()) throw new ValidationException(validationFailures);

        return await next();
    }
}