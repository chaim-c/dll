using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000009 RID: 9
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ChannelMessageReceivedMessage : Message
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000021AB File Offset: 0x000003AB
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000021B3 File Offset: 0x000003B3
		[JsonProperty]
		public ChatChannelType Channel { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000021BC File Offset: 0x000003BC
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000021C4 File Offset: 0x000003C4
		[JsonProperty]
		public string PlayerName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021D5 File Offset: 0x000003D5
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000029 RID: 41 RVA: 0x000021DE File Offset: 0x000003DE
		public ChannelMessageReceivedMessage()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000021E6 File Offset: 0x000003E6
		public ChannelMessageReceivedMessage(ChatChannelType channel, string playerName, string message)
		{
			this.Channel = channel;
			this.PlayerName = playerName;
			this.Message = message;
		}
	}
}
