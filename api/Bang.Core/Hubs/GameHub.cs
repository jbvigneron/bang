using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Bang.Core.Hubs
{
    [SignalRHub]
    public class GameHub : Hub
    {
        public Task Subscribe(Guid gameId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId.ToString());
        }
    }
}
