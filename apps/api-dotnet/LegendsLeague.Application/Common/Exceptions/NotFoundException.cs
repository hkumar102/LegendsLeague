using System.Runtime.Serialization;

namespace LegendsLeague.Application.Common.Exceptions;

/// <summary>
/// Represents a 404 Not Found domain error that controllers can map to HTTP 404.
/// </summary>
[Serializable]
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception? inner) : base(message, inner) { }
    private NotFoundException(SerializationInfo info, StreamingContext ctx) : base(info, ctx) { }
}
