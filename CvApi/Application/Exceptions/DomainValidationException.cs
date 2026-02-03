using System;
using FluentValidation.Results;

namespace CvApi.Application.Exceptions;

/// <summary>Validation exception class.</summary>
[Serializable]
public class DomainValidationException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="DomainValidationException" /> class.</summary>
    public DomainValidationException()
    {
        Failures = [];
    }

    /// <summary>Initializes a new instance of the <see cref="DomainValidationException" /> class.</summary>
    /// <param name="message">The message.</param>
    /// <param name="failures">The failures.</param>
    public DomainValidationException(string message, ValidationFailure[] failures)
        : base(message)
    {
        Failures = failures;
    }

    /// <summary>Initializes a new instance of the <see cref="DomainValidationException" /> class.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    ///     The exception that is the cause of the current exception
    /// </param>
    public DomainValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
        Failures = [];
    }

    /// <summary>Initializes a new instance of the <see cref="DomainValidationException" /> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public DomainValidationException(string message) : base(message)
    {
        Failures = [];
    }

    /// <summary>Gets the failures.</summary>
    /// <value>The failures.</value>
    public ValidationFailure[] Failures { get; }
}