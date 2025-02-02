using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B4 RID: 180
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class LeaveChannelMessage : Message
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000434E File Offset: 0x0000254E
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00004356 File Offset: 0x00002556
		[JsonProperty]
		public ChatChannelType Channel { get; private set; }

		// Token: 0x0600033B RID: 827 RVA: 0x0000435F File Offset: 0x0000255F
		public LeaveChannelMessage()
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00004367 File Offset: 0x00002567
		public LeaveChannelMessage(ChatChannelType channel)
		{
			this.Channel = channel;
		}
	}
}
