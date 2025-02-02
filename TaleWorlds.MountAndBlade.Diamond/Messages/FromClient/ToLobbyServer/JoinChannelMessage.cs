using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000B1 RID: 177
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class JoinChannelMessage : Message
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000042D6 File Offset: 0x000024D6
		// (set) Token: 0x0600032E RID: 814 RVA: 0x000042DE File Offset: 0x000024DE
		[JsonProperty]
		public ChatChannelType Channel { get; private set; }

		// Token: 0x0600032F RID: 815 RVA: 0x000042E7 File Offset: 0x000024E7
		public JoinChannelMessage()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000042EF File Offset: 0x000024EF
		public JoinChannelMessage(ChatChannelType channel)
		{
			this.Channel = channel;
		}
	}
}
