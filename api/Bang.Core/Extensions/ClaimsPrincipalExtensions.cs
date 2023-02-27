using Bang.Core.Constants;
using System.Security.Claims;

namespace Bang.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user) =>
            Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public static string GetName(this ClaimsPrincipal user) =>
            user.FindFirst(ClaimTypes.Name)!.Value;

        public static Guid GetGameId(this ClaimsPrincipal user) =>
            Guid.Parse(user.FindFirst(JwtConstants.GameId)!.Value);
    }
}
