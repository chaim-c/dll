using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000039 RID: 57
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class JoinBattleMessage : Message
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002AC5 File Offset: 0x00000CC5
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002ACD File Offset: 0x00000CCD
		[JsonProperty]
		public BattleServerInformationForClient BattleServerInformation { get; private set; }

		// Token: 0x06000105 RID: 261 RVA: 0x00002AD6 File Offset: 0x00000CD6
		public JoinBattleMessage()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002ADE File Offset: 0x00000CDE
		public JoinBattleMessage(BattleServerInformationForClient battleServerInformation)
		{
			this.BattleServerInformation = battleServerInformation;
		}
	}
}
