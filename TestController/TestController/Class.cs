using Microsoft.AspNetCore.SignalR;

namespace TestController
{
	public class MainHub : Hub
	{
		private static readonly Dictionary<string, string> ConnectionsGroup = new Dictionary<string, string>();

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			if (ConnectionsGroup.ContainsKey(Context.ConnectionId))
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConnectionsGroup[Context.ConnectionId]);
				ConnectionsGroup.Remove(Context.ConnectionId);
			}
			await base.OnDisconnectedAsync(exception);
		}

		public async Task JoinGroup(string group)
		{
			if (ConnectionsGroup.ContainsKey(Context.ConnectionId))
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, ConnectionsGroup[Context.ConnectionId]);
				ConnectionsGroup.Remove(Context.ConnectionId);
			}
			ConnectionsGroup.Add(Context.ConnectionId, group);
			await Groups.AddToGroupAsync(Context.ConnectionId, group);
		}

		public async Task SendMessage(string message)
		{
			await Clients.Group("groupId")
						 .SendAsync("onMessage", message);
		}

	}


	}

