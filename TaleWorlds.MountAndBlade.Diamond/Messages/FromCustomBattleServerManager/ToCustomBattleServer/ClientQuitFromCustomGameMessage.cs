using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000067 RID: 103
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[Serializable]
	public class ClientQuitFromCustomGameMessage : Message
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00003788 File Offset: 0x00001988
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00003790 File Offset: 0x00001990
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x0600021E RID: 542 RVA: 0x00003799 File Offset: 0x00001999
		public ClientQuitFromCustomGameMessage()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000037A1 File Offset: 0x000019A1
		public ClientQuitFromCustomGameMessage(PlayerId playerId)
		{
			this.PlayerId = playerId;
		}
	}
}
