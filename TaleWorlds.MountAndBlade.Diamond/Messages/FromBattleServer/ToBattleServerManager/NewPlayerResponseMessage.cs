using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000DD RID: 221
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class NewPlayerResponseMessage : Message
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00004CC9 File Offset: 0x00002EC9
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00004CD1 File Offset: 0x00002ED1
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00004CDA File Offset: 0x00002EDA
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00004CE2 File Offset: 0x00002EE2
		[JsonProperty]
		public PlayerBattleServerInformation PlayerBattleInformation { get; private set; }

		// Token: 0x06000415 RID: 1045 RVA: 0x00004CEB File Offset: 0x00002EEB
		public NewPlayerResponseMessage()
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00004CF3 File Offset: 0x00002EF3
		public NewPlayerResponseMessage(PlayerId playerId, PlayerBattleServerInformation playerBattleInformation)
		{
			this.PlayerId = playerId;
			this.PlayerBattleInformation = playerBattleInformation;
		}
	}
}
