using System.Runtime.Serialization;

namespace LegendsLeague.Application.Common.Exceptions;

/// <summary>
/// Represents a 409 Conflict error from the Application layer,
/// typically used when creating/updating resources would violate a uniqueness rule.
/// Controllers can translate this to HTTP 409.
/// </summary>
[Serializable]
public sealed class ConflictException : Exception
{
    /// <summary>Creates a new conflict exception with a message.</summary>
    public ConflictException(string message) : base(message) { }

    /// <summary>Creates a new conflict exception with a message and inner exception.</summary>
    public ConflictException(string message, Exception? inner) : base(message, inner) { }

    private ConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
