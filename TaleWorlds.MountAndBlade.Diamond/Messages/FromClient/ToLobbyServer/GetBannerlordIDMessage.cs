using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000099 RID: 153
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetBannerlordIDMessage : Message
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00003F7D File Offset: 0x0000217D
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00003F85 File Offset: 0x00002185
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002DC RID: 732 RVA: 0x00003F8E File Offset: 0x0000218E
		public GetBannerlordIDMessage()
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00003F96 File Offset: 0x00002196
		public GetBannerlordIDMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
