using Bang.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Bang.WebApi.Middlewares
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IMediator mediator;

        public CustomCookieAuthenticationEvents(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var principal = context.Principal!;
            var playerId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var query = new PlayerQuery(playerId);
            var player = await this.mediator.Send(query);

            if (player == null || player.Id != playerId)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
