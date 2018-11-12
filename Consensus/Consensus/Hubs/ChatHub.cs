using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Consensus.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Guid userId, string userName, string message, DateTimeOffset sentOn)
        {
            await Clients.All.SendAsync("Message", userId, userName, sentOn, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "users");
            await base.OnConnectedAsync();
        }
    }
}