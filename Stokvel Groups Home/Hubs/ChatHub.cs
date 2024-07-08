
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


	public Task JoinGroup(string group)
	{
		return Groups.AddToGroupAsync(Context.ConnectionId, group);
	}

	public Task SendMessageToGroup(string group, Message message)
	{
		return Clients.Group(group).SendAsync("receiveMessage", message);
	}
}
