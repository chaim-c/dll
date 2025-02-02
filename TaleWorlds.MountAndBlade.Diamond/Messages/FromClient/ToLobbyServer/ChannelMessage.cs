using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000083 RID: 131
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChannelMessage : Message
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00003BDF File Offset: 0x00001DDF
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00003BE7 File Offset: 0x00001DE7
		[JsonProperty]
		public ChatChannelType Channel { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00003BF0 File Offset: 0x00001DF0
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00003BF8 File Offset: 0x00001DF8
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x06000286 RID: 646 RVA: 0x00003C01 File Offset: 0x00001E01
		public ChannelMessage()
		{
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003C09 File Offset: 0x00001E09
		public ChannelMessage(ChatChannelType channel, string message)
		{
			this.Channel = channel;
			this.Message = message;
		}
	}
}
