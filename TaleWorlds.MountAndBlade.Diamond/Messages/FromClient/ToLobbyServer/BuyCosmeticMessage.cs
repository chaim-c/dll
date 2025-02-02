using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x0200007A RID: 122
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class BuyCosmeticMessage : Message
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00003AB7 File Offset: 0x00001CB7
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00003ABF File Offset: 0x00001CBF
		[JsonProperty]
		public string CosmeticId { get; private set; }

		// Token: 0x06000266 RID: 614 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public BuyCosmeticMessage()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public BuyCosmeticMessage(string cosmeticId)
		{
			this.CosmeticId = cosmeticId;
		}
	}
}
