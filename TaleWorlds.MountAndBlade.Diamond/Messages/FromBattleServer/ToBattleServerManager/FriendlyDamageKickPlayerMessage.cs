using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000DB RID: 219
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class FriendlyDamageKickPlayerMessage : Message
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00004C81 File Offset: 0x00002E81
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00004C89 File Offset: 0x00002E89
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00004C92 File Offset: 0x00002E92
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x00004C9A File Offset: 0x00002E9A
		[TupleElementNames(new string[]
		{
			"killCount",
			"damage"
		})]
		[JsonProperty]
		public Dictionary<int, ValueTuple<int, float>> RoundDamageMap { [return: TupleElementNames(new string[]
		{
			"killCount",
			"damage"
		})] get; [param: TupleElementNames(new string[]
		{
			"killCount",
			"damage"
		})] private set; }

		// Token: 0x0600040E RID: 1038 RVA: 0x00004CA3 File Offset: 0x00002EA3
		public FriendlyDamageKickPlayerMessage()
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00004CAB File Offset: 0x00002EAB
		public FriendlyDamageKickPlayerMessage(PlayerId playerId, [TupleElementNames(new string[]
		{
			"killCount",
			"damage"
		})] Dictionary<int, ValueTuple<int, float>> roundDamageMap)
		{
			this.PlayerId = playerId;
			this.RoundDamageMap = roundDamageMap;
		}
	}
}
