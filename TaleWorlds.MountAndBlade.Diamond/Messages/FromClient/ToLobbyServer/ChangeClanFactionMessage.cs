using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007D RID: 125
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangeClanFactionMessage : Message
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00003AEF File Offset: 0x00001CEF
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00003AF7 File Offset: 0x00001CF7
		[JsonProperty]
		public string NewFaction { get; private set; }

		// Token: 0x0600026C RID: 620 RVA: 0x00003B00 File Offset: 0x00001D00
		public ChangeClanFactionMessage()
		{
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00003B08 File Offset: 0x00001D08
		public ChangeClanFactionMessage(string newFaction)
		{
			this.NewFaction = newFaction;
		}
	}
}
