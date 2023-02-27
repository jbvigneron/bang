using Microsoft.AspNetCore.SignalR;

namespace Bang.Core.Hubs
{
    public class GameHub : Hub
    {
        public Task Subscribe(Guid gameId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId.ToString());
        }
    }
}
