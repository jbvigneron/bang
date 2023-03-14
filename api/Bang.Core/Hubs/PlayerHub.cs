using Bang.Core.Constants;
using Bang.Core.Extensions;
using Bang.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.Hubs
{
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
            var playerId = this.Context.User.GetId();
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, this.Context.User.GetId().ToString());

            var cards = await this.mediator.Send(new PlayerDeckQuery(this.Context.User));
            await this.Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.YourHand, cards);
        }
    }
}
