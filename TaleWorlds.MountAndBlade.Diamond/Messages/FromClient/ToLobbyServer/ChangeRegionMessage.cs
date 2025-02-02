using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000081 RID: 129
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangeRegionMessage : Message
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00003B8F File Offset: 0x00001D8F
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00003B97 File Offset: 0x00001D97
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x0600027C RID: 636 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public ChangeRegionMessage()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public ChangeRegionMessage(string region)
		{
			this.Region = region;
		}
	}
}
