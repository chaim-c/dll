using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000A2 RID: 162
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class GetPlayerClanInfo : Message
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000403D File Offset: 0x0000223D
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00004045 File Offset: 0x00002245
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x0000404E File Offset: 0x0000224E
		public GetPlayerClanInfo()
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00004056 File Offset: 0x00002256
		public GetPlayerClanInfo(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
