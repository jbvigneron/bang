using Bang.Core.Constants;
using System.Security.Claims;

namespace Bang.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user) =>
            Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

        public static string GetName(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.Name);

        public static Guid GetGameId(this ClaimsPrincipal user) =>
            Guid.Parse(user.FindFirstValue(JwtConstants.GameId));
    }
}
