using CvApi.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CvApi.Middleware;

/// <summary>Filter to format validation exceptions</summary>
/// <remarks>Initializes a new instance of the <see cref="ApplicationExceptionFilter" /> class.</remarks>
/// <param name="env">The env.</param>
/// <param name="logger">The logger.</param>
public class ApplicationExceptionFilter(IWebHostEnvironment env, ILogger<ApplicationExceptionFilter> logger) : IExceptionFilter
{
    /// <summary>Called after an action has thrown an <see cref="T:System.Exception">Exception</see>.</summary>
    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext">ExceptionContext</see>.</param>
    public void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            "{ExceptionMessage}",
            context.Exception.Message);

        switch (context.Exception)
        {
            case DomainValidationException domainValidationException:
                HandleDomainValidationException(context, domainValidationException);

                break;
            default:
            {
                var json = new JsonErrorResponse { Messages = ["An error occur."] };

                ExtendMessageInDevEnvironment(context, json);

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                break;
            }
        }

        context.ExceptionHandled = true;
    }

    private void ExtendMessageInDevEnvironment(ExceptionContext context, JsonErrorResponse json)
    {
        if (!env.IsProduction())
        {
            var ex = context.Exception;

            if (ex != null)
            {
                var errorType = ex.GetType().FullName ?? "Unknown type";

                json.DeveloperMessage = new DeveloperMessage(
                    ex.Message,
                    errorType,
                    ex.InnerException?.Message ?? string.Empty,
                    ex.StackTrace ?? string.Empty);
            }
        }
    }

    private static void HandleDomainValidationException(
        ExceptionContext context,
        DomainValidationException domainValidationException)
    {
        var errorMessages = domainValidationException.Failures
            .Select(info => info.ErrorMessage)
            .ToArray();

        var validationProblems = errorMessages.Length != 0 ? errorMessages : [domainValidationException.Message];

        var problemDetails = new ValidationProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        problemDetails.Errors.Add("validation", [.. validationProblems]);

        context.Result = new BadRequestObjectResult(problemDetails);
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }

    private sealed class JsonErrorResponse
    {
        public string[] Messages { get; init; } = default!;

        public DeveloperMessage DeveloperMessage { get; set; }
    }

    private sealed record DeveloperMessage(
        string Message,
        string ErrorType,
        string InnerExceptionMessage,
        string CallStack);
}