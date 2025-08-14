namespace LegendsLeague.Application.Abstractions.Security;

/// <summary>
/// Provides information about the current authenticated principal for the ongoing request.
/// API is expected to supply an implementation that reads from the HTTP context (e.g., JWT claims).
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the unique identifier of the current user, if available.
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// Returns <c>true</c> when a meaningful user identity is available (e.g., authenticated).
    /// </summary>
    bool IsAuthenticated { get; }
}
