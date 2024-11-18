using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using notify.Dtos;
using notify.Interfaces;

namespace notify.Hubs;

public class NotificationHubClient : Hub<INotificationHubClient>
{
    internal static readonly ConcurrentDictionary<Guid, string> ConnectedUsers = new();

    public override async Task OnConnectedAsync()
    {
        var userIdQuery = Context.GetHttpContext()?.Request.Query["userId"].ToString();

        if (Guid.TryParse(userIdQuery, out var userId))
        {
            ConnectedUsers[userId] = Context.ConnectionId;

            await Clients.All.ReceiveOnlineSubscribers(ConnectedUsers.Keys);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = ConnectedUsers.FirstOrDefault(pair => pair.Value == Context.ConnectionId);
        if (!user.Equals(default(KeyValuePair<Guid, string>)))
        {
            ConnectedUsers.TryRemove(user.Key, out _);
        }

        await base.OnDisconnectedAsync(exception);
    }
}
