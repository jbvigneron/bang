using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Bang.Core.Hubs
{
    [Authorize]
    public class PlayerHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var principal = this.Context.User;
            var playerId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, playerId.ToString());
            await base.OnConnectedAsync();
        }
    }
}
