//using Dokaanah.BusinessLogicLayer.Repositories.RepoInterfaces;
//using Microsoft.AspNetCore.SignalR;
//using System.Collections.Concurrent;

//public class UserHubdd : Hub
//{

//    private readonly IUserConnectionManager _connectionManager;

//    public UserHubdd(IUserConnectionManager connectionManager)
//    {
//        _connectionManager = connectionManager;
//    }

//    public override Task OnConnectedAsync()
//    {
//        var userId = Context.UserIdentifier; // Use authenticated user's ID
//        if (!string.IsNullOrEmpty(userId))
//        {
//            _connectionManager.AddConnection(userId, Context.ConnectionId);
//            Clients.All.SendAsync("UpdateUserStatus", userId, "Online");
//        }
//        return base.OnConnectedAsync();
//    }

//    public override Task OnDisconnectedAsync(Exception? exception)
//    {
//        _connectionManager.RemoveConnection(Context.ConnectionId);
//        Clients.All.SendAsync("UpdateUserStatus", Context.UserIdentifier, "Offline");
//        return base.OnDisconnectedAsync(exception);
//    }

//}
