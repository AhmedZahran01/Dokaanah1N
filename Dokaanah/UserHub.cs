using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
 
[Authorize(Roles = "Admin")]
public class UserHub : Hub
{ 
    public async Task UpdateUserStatus(string userId, string status)
    {
        await Clients.All.SendAsync("ReceiveUserStatus", userId, status);
    }

}



#region Comment Region





//@*  <tbody id="userList">
//@foreach (var customer in allCustomers2)
//{
//<tr data-user-id="@customer.Id">
//<td>@customer.Id</td>
//<td>@customer.UserName</td>
//<td>@customer.Email</td>
//<td id="StatusIdSc" class="status-cell">
//@if (customer.IsOnline)
//{
//<span class="badge bg-success"> Online </span>
//}
//else
//{
//<span class="badge bg-danger"> Offline </span>
//}
//</td>
//<td>@customer.Address</td>
//<td>
//<a class="btn btn-primary btn-sm" asp-controller="Customers" asp-action="EditByAdmin" asp-route-id="@customer.Id">
//<i class="fas fa-edit"></i>
//</a>
//<a class="btn btn-danger btn-sm" asp-controller="Customers" asp-action="Delete" asp-route-id="@customer.Id">
//<i class="fas fa-trash"></i>
//</a>
//</td>
//</tr>
//}
//</tbody>
//@


//public async Task SendMessage(string userId, string status)
//{
//    await Clients.All.SendAsync("ReceiveUserStatus", userId, status);
//}

//using Microsoft.AspNetCore.SignalR;

//public class UserHub : Hub
//{
//    public async Task UpdateUserStatus(string userId, string status)
//    {
//        await Clients.All.SendAsync("ReceiveUserStatus", userId, status);
//    }

//    public override async Task OnConnectedAsync()
//    {
//        var userId = Context.UserIdentifier; // لازم يكون المستخدم Authenticated
//        if (!string.IsNullOrEmpty(userId))
//        {
//            await Clients.All.SendAsync("ReceiveUserStatus", userId, "Online");
//        }
//        await base.OnConnectedAsync();
//    }

//    public override async Task OnDisconnectedAsync(Exception? exception)
//    {
//        var userId = Context.UserIdentifier;
//        if (!string.IsNullOrEmpty(userId))
//        {
//            await Clients.All.SendAsync("ReceiveUserStatus", userId, "Offline");
//        }
//        await base.OnDisconnectedAsync(exception);
//    }


//}





//using Microsoft.AspNetCore.SignalR;

//public class ChatHub : Hub
//{

//    public async Task SendMessage(string User, string message)
//    {
//       await Clients.All.SendAsync("RecievedMessage", User, message);
//    }


//    public override async Task OnConnectedAsync()
//    {
//        var userId = Context.UserIdentifier; // لازم يكون المستخدم Authenticated
//        if (!string.IsNullOrEmpty(userId))
//        {
//            await Clients.All.SendAsync("ReceiveUserStatus", userId, "Online");
//        }
//        await base.OnConnectedAsync();
//    }

//    //    public override async Task OnDisconnectedAsync(Exception? exception)
//    //    {
//    //        var userId = Context.UserIdentifier;
//    //        if (!string.IsNullOrEmpty(userId))
//    //        {
//    //            await Clients.All.SendAsync("ReceiveUserStatus", userId, "Offline");
//    //        }
//    //        await base.OnDisconnectedAsync(exception);
//    //    }

//} 


#endregion