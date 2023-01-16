using Bang.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Bang.Core.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private readonly IMediator mediator;

        public GameHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var principal = this.Context.User;
            var playerId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
            var game = await this.mediator.Send(new GameFromPlayerIdQuery(playerId));

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, game.Id.ToString());
            await base.OnConnectedAsync();
        }
    }
}
