using Bang.Core.Constants;
using Bang.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Security.Claims;

namespace Bang.Core.Hubs
{
    [SignalRHub]
    [Authorize]
    public class PlayerHub : Hub
    {
        private readonly IMediator mediator;

        public PlayerHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Subscribe()
        {
            var principal = this.Context.User;
            var playerId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, playerId.ToString());

            var cards = await this.mediator.Send(new PlayerDeckQuery(playerId));

            await this.Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.CardsInHand, cards);
        }
    }
}
