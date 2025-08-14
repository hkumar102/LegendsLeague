using System.Security.Claims;
using LegendsLeague.Application.Abstractions.Security;
using Microsoft.AspNetCore.Http;

namespace LegendsLeague.Api.Security;

/// <summary>
/// Default implementation of <see cref="ICurrentUser"/> that resolves the current user
/// from the ASP.NET Core <see cref="HttpContext"/>.
/// In development, this also supports an <c>X-User-Id</c> header (GUID) for quick testing.
/// </summary>
public sealed class CurrentUserAccessor : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUserAccessor"/> class.
    /// </summary>
    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        => _http = httpContextAccessor;

    /// <inheritdoc />
    public Guid? UserId
    {
        get
        {
            var ctx = _http.HttpContext;
            if (ctx is null) return null;

            // 1) Try standard claim types first (adjust to your IdP as needed)
            var idClaim = ctx.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                          ?? ctx.User?.FindFirst("sub")?.Value;

            if (Guid.TryParse(idClaim, out var fromClaims))
                return fromClaims;

            // 2) Dev convenience: X-User-Id header (GUID)
            if (ctx.Request.Headers.TryGetValue("X-User-Id", out var headerVals))
            {
                var raw = headerVals.FirstOrDefault();
                if (Guid.TryParse(raw, out var fromHeader))
                    return fromHeader;
            }

            // 3) Optional dev fallback: a fixed system user (comment out in prod)
            // return Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            return null;
        }
    }

    /// <inheritdoc />
    public bool IsAuthenticated => UserId.HasValue;
}
