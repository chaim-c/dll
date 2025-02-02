using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BE RID: 190
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RemoveClanOfficerRoleForPlayerMessage : Message
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00004570 File Offset: 0x00002770
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00004578 File Offset: 0x00002778
		[JsonProperty]
		public PlayerId RemovedOfficerId { get; private set; }

		// Token: 0x0600036D RID: 877 RVA: 0x00004581 File Offset: 0x00002781
		public RemoveClanOfficerRoleForPlayerMessage()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00004589 File Offset: 0x00002789
		public RemoveClanOfficerRoleForPlayerMessage(PlayerId removedOfficerId)
		{
			this.RemovedOfficerId = removedOfficerId;
		}
	}
}
