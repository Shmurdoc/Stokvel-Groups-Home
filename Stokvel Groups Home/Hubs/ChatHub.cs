
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Hubs;


public class ChatHub : Hub
{
	public async Task SendMessage(Message message) =>
		await Clients.All.SendAsync("receiveMessage", message);


	public async Task JoinGroup(string group)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, group);
	}

	public Task SendMessageToGroup(string group, Message message)
	{
		return Clients.Group(group).SendAsync("receiveMessage", message);
	}

	public Task SendMessageToCaller(Message message)
	{
		return Clients.Caller.SendAsync("ReceiveMessage", message);
	}
	public Task SendMessageToUser(string connectionId, Message message)
	{
		return Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
	}
	
	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
		await base.OnConnectedAsync();
	}
	public override async Task OnDisconnectedAsync(Exception ex)
	{
		await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
		await base.OnDisconnectedAsync(ex);
	}
	public Task SendPrivateMessage(string user, Message message)
	{
		this.SendMessageToCaller(message);
		return Clients.User(user).SendAsync("ReceiveMessage", message);
	}
}
