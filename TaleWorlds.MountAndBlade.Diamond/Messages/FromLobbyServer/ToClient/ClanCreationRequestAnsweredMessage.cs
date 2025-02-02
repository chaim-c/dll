using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200000E RID: 14
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class ClanCreationRequestAnsweredMessage : Message
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000022A3 File Offset: 0x000004A3
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000022B4 File Offset: 0x000004B4
		[JsonProperty]
		public ClanCreationAnswer ClanCreationAnswer { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x000022BD File Offset: 0x000004BD
		public ClanCreationRequestAnsweredMessage()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000022C5 File Offset: 0x000004C5
		public ClanCreationRequestAnsweredMessage(PlayerId playerId, ClanCreationAnswer clanCreationAnswer)
		{
			this.PlayerId = playerId;
			this.ClanCreationAnswer = clanCreationAnswer;
		}
	}
}
