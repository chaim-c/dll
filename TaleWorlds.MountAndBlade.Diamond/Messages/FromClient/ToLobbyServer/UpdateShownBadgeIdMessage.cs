using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CE RID: 206
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateShownBadgeIdMessage : Message
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000048DA File Offset: 0x00002ADA
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000048E2 File Offset: 0x00002AE2
		[JsonProperty]
		public string ShownBadgeId { get; private set; }

		// Token: 0x060003C0 RID: 960 RVA: 0x000048EB File Offset: 0x00002AEB
		public UpdateShownBadgeIdMessage()
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000048F3 File Offset: 0x00002AF3
		public UpdateShownBadgeIdMessage(string shownBadgeId)
		{
			this.ShownBadgeId = shownBadgeId;
		}
	}
}
