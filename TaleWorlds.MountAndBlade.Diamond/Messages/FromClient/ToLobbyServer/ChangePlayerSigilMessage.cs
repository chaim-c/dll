using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000080 RID: 128
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangePlayerSigilMessage : Message
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00003B67 File Offset: 0x00001D67
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00003B6F File Offset: 0x00001D6F
		[JsonProperty]
		public string SigilId { get; private set; }

		// Token: 0x06000278 RID: 632 RVA: 0x00003B78 File Offset: 0x00001D78
		public ChangePlayerSigilMessage()
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00003B80 File Offset: 0x00001D80
		public ChangePlayerSigilMessage(string sigilId)
		{
			this.SigilId = sigilId;
		}
	}
}
