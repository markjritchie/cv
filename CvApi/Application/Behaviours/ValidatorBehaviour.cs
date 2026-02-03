using CvApi.Application.Exceptions;
using CvApi.Application.Extensions;
using FluentValidation;
using MediatR;

namespace CvApi.Application.Behaviours;

/// <summary>Validation behaviour for MediatR</summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidatorBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<ValidatorBehaviour<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>Initializes a new instance of the <see cref="ValidatorBehaviour{TRequest, TResponse}" /> class.</summary>
    /// <param name="validators">The validators.</param>
    /// <param name="logger">The logger.</param>
    public ValidatorBehaviour(IEnumerable<IValidator<TRequest>> validators,
        ILogger<ValidatorBehaviour<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    /// <summary>Pipeline handler. Perform any additional behavior and await the <paramref name="next" /> delegate as necessary</summary>
    /// <param name="request">Incoming request</param>
    /// <param name="next">
    ///     Awaitable delegate for the next action in the pipeline. Eventually this delegate represents the
    ///     handler.
    /// </param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Awaitable task returning the <span class="typeparameter">TResponse</span></returns>
    /// <exception cref="DomainValidationException">Command Validation Errors for type {typeof(TRequest).Name}</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var typeName = request.GetGenericTypeName();

        _logger.LogInformation("----- Validating command {CommandType}", typeName);

        var validationResults = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(request, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToArray();

        if (failures.Length > 0)
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                typeName, request, failures);

            throw new DomainValidationException($"Command Validation Errors for type {typeof(TRequest).Name}",
                failures);
        }

        return await next();
    }
}