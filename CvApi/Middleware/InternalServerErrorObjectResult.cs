using Microsoft.AspNetCore.Mvc;

namespace CvApi.Middleware;

/// <summary>Result when there's an internal error</summary>
public class InternalServerErrorObjectResult : ObjectResult
{
    /// <summary>Initializes a new instance of the <see cref="InternalServerErrorObjectResult" /> class.</summary>
    /// <param name="error">The error.</param>
    public InternalServerErrorObjectResult(object error) : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}