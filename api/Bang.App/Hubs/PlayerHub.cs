using Bang.App.Constants;
using Bang.Domain.Extensions;
using Bang.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bang.App.Hubs
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

            var cards = await this.mediator.Send(new PlayerHandQuery(this.Context.User));
            await this.Clients.Group(playerId.ToString())
                .SendAsync(HubMessages.Player.YourHand, cards);
        }
    }
}