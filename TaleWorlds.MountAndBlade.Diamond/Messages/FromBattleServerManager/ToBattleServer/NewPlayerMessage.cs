using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E6 RID: 230
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class NewPlayerMessage : Message
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00004E42 File Offset: 0x00003042
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00004E4A File Offset: 0x0000304A
		[JsonProperty]
		public PlayerBattleInfo PlayerBattleInfo { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00004E53 File Offset: 0x00003053
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00004E5B File Offset: 0x0000305B
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00004E64 File Offset: 0x00003064
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00004E6C File Offset: 0x0000306C
		[JsonProperty]
		public Guid PlayerParty { get; private set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00004E75 File Offset: 0x00003075
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00004E7D File Offset: 0x0000307D
		[JsonProperty]
		public Dictionary<string, List<string>> UsedCosmetics { get; private set; }

		// Token: 0x0600043D RID: 1085 RVA: 0x00004E86 File Offset: 0x00003086
		public NewPlayerMessage()
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00004E8E File Offset: 0x0000308E
		public NewPlayerMessage(PlayerData playerData, PlayerBattleInfo playerBattleInfo, Guid playerParty, Dictionary<string, List<string>> usedCosmetics)
		{
			this.PlayerBattleInfo = playerBattleInfo;
			this.PlayerData = playerData;
			this.PlayerParty = playerParty;
			this.UsedCosmetics = usedCosmetics;
		}
	}
}
