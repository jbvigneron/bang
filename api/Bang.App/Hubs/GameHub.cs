using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Bang.App.Hubs
{
    public class GameHub : Hub
    {
        public Task Subscribe(Guid gameId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId.ToString());
        }
    }
}