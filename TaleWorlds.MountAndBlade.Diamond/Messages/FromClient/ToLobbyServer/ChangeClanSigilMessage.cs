using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007E RID: 126
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ChangeClanSigilMessage : Message
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003B17 File Offset: 0x00001D17
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00003B1F File Offset: 0x00001D1F
		[JsonProperty]
		public string NewSigil { get; private set; }

		// Token: 0x06000270 RID: 624 RVA: 0x00003B28 File Offset: 0x00001D28
		public ChangeClanSigilMessage()
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00003B30 File Offset: 0x00001D30
		public ChangeClanSigilMessage(string newSigil)
		{
			this.NewSigil = newSigil;
		}
	}
}
