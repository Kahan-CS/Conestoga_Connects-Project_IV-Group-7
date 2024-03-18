using BlazorApp.Shared;
using Microsoft.AspNetCore.SignalR;

namespace BlazorApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(MessageModel model)
        {
            await Clients.All.SendAsync("ReceiveMessage", model); 
        }
    }
}
