using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Diamond.ChatSystem.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000102 RID: 258
	public class ChatManager
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00006E85 File Offset: 0x00005085
		public ChatManager(IChatClientHandler chatClientHandler)
		{
			this._clients = new Dictionary<string, ChatClient>();
			this._rooms = new Dictionary<Guid, ChatRoomInformationForClient>();
			this._chatClientHandler = chatClientHandler;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00006EAC File Offset: 0x000050AC
		public void OnJoinChatRoom(ChatRoomInformationForClient info, PlayerId playerId, string playerName)
		{
			if (this._rooms.ContainsKey(info.RoomId))
			{
				return;
			}
			if (!this._clients.ContainsKey(info.Endpoint))
			{
				ChatClient chatClient = new ChatClient(info.Endpoint, playerId.ToString(), playerName, info.RoomId);
				this._clients.Add(info.Endpoint, chatClient);
				chatClient.OnMessageReceived += this.ClientOnMessageReceived;
				chatClient.Connect();
			}
			this._rooms.Add(info.RoomId, info);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00006F3C File Offset: 0x0000513C
		public void OnTick()
		{
			if (!this._isCleaningUp)
			{
				List<string> list = null;
				foreach (string text in this._clients.Keys)
				{
					ChatClient chatClient = this._clients[text];
					chatClient.OnTick();
					if (chatClient.State == ChatClientState.Stopped)
					{
						if (list == null)
						{
							list = new List<string>(1);
						}
						list.Add(text);
					}
				}
				if (list != null)
				{
					foreach (string key in list)
					{
						ChatClient chatClient2 = this._clients[key];
						this._rooms.Remove(chatClient2.RoomId);
						this._clients.Remove(key);
					}
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00007030 File Offset: 0x00005230
		public List<ChatRoomInformationForClient> Rooms
		{
			get
			{
				return this._rooms.Values.ToList<ChatRoomInformationForClient>();
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00007044 File Offset: 0x00005244
		public void OnChatRoomClosed(Guid roomId)
		{
			ChatRoomInformationForClient room;
			if (this._rooms.TryGetValue(roomId, out room))
			{
				room = this._rooms[roomId];
				this._rooms.Remove(roomId);
				if (!this._rooms.Any((KeyValuePair<Guid, ChatRoomInformationForClient> item) => room.Endpoint == item.Value.Endpoint))
				{
					this._clients[room.Endpoint].Stop();
					this._clients.Remove(room.Endpoint);
				}
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000070D8 File Offset: 0x000052D8
		private void ClientOnMessageReceived(ChatClient client, ChatMessage message)
		{
			ChatRoomInformationForClient chatRoomInformationForClient;
			if (this._rooms.TryGetValue(message.RoomId, out chatRoomInformationForClient))
			{
				this._chatClientHandler.OnChatMessageReceived(message.RoomId, chatRoomInformationForClient.Name, message.Name, message.Text, chatRoomInformationForClient.RoomColor, message.Type);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000712C File Offset: 0x0000532C
		public async void SendMessage(Guid roomId, string message)
		{
			ChatRoomInformationForClient chatRoomInformationForClient;
			if (this._rooms.TryGetValue(roomId, out chatRoomInformationForClient))
			{
				string endpoint = chatRoomInformationForClient.Endpoint;
				ChatClient chatClient;
				if (this._clients.TryGetValue(endpoint, out chatClient))
				{
					await chatClient.Send(new ChatMessage
					{
						RoomId = roomId,
						Text = message
					});
				}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00007178 File Offset: 0x00005378
		public async void Cleanup()
		{
			this._isCleaningUp = true;
			foreach (KeyValuePair<string, ChatClient> keyValuePair in this._clients)
			{
				await keyValuePair.Value.Stop();
			}
			Dictionary<string, ChatClient>.Enumerator enumerator = default(Dictionary<string, ChatClient>.Enumerator);
			this._clients.Clear();
			this._rooms.Clear();
			this._isCleaningUp = false;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000071B4 File Offset: 0x000053B4
		public ChatManager.GetChatRoomResult TryGetChatRoom(string command)
		{
			if (!command.StartsWith("/"))
			{
				return ChatManager.GetChatRoomResult.CreateFailed(new TextObject("{=taPBAd4c}Given command does not start with /", null));
			}
			string value = command.ToLower().Split(new char[]
			{
				'/'
			}).Last<string>();
			List<ChatRoomInformationForClient> list = new List<ChatRoomInformationForClient>();
			foreach (ChatRoomInformationForClient chatRoomInformationForClient in this._rooms.Values)
			{
				if (chatRoomInformationForClient.Name.ToLower().StartsWith(value))
				{
					list.Add(chatRoomInformationForClient);
				}
			}
			if (list.Count == 1)
			{
				return ChatManager.GetChatRoomResult.CreateSuccessful(list[0]);
			}
			if (list.Count == 0)
			{
				TextObject textObject = new TextObject("{=YOYvBVu1}No chat room found matching {COMMAND}", null);
				textObject.SetTextVariable("COMMAND", command);
				return ChatManager.GetChatRoomResult.CreateFailed(textObject);
			}
			TextObject textObject2 = new TextObject("{=6doiovtH}Disambiguation: {CHATROOMS}", null);
			string text = "";
			for (int i = 0; i < list.Count; i++)
			{
				text += list[i];
				if (i != list.Count - 1)
				{
					text += ", ";
				}
			}
			textObject2.SetTextVariable("CHATROOMS", text);
			return ChatManager.GetChatRoomResult.CreateFailed(textObject2);
		}

		// Token: 0x040001F7 RID: 503
		private readonly Dictionary<string, ChatClient> _clients;

		// Token: 0x040001F8 RID: 504
		private readonly Dictionary<Guid, ChatRoomInformationForClient> _rooms;

		// Token: 0x040001F9 RID: 505
		private readonly IChatClientHandler _chatClientHandler;

		// Token: 0x040001FA RID: 506
		private bool _isCleaningUp;

		// Token: 0x02000198 RID: 408
		public class GetChatRoomResult
		{
			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00012EB1 File Offset: 0x000110B1
			// (set) Token: 0x06000B06 RID: 2822 RVA: 0x00012EB9 File Offset: 0x000110B9
			public bool Successful { get; private set; }

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00012EC2 File Offset: 0x000110C2
			// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00012ECA File Offset: 0x000110CA
			public ChatRoomInformationForClient Room { get; private set; }

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00012ED3 File Offset: 0x000110D3
			// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00012EDB File Offset: 0x000110DB
			public TextObject ErrorMessage { get; private set; }

			// Token: 0x06000B0B RID: 2827 RVA: 0x00012EE4 File Offset: 0x000110E4
			public GetChatRoomResult(bool successful, ChatRoomInformationForClient room, TextObject error)
			{
				this.Successful = successful;
				this.Room = room;
				this.ErrorMessage = error;
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x00012F01 File Offset: 0x00011101
			public static ChatManager.GetChatRoomResult CreateSuccessful(ChatRoomInformationForClient room)
			{
				return new ChatManager.GetChatRoomResult(true, room, new TextObject("", null));
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x00012F15 File Offset: 0x00011115
			public static ChatManager.GetChatRoomResult CreateFailed(TextObject error)
			{
				return new ChatManager.GetChatRoomResult(false, null, error);
			}
		}
	}
}
