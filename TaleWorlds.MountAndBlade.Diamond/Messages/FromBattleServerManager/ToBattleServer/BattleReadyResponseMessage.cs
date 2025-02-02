using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromBattleServerManager.ToBattleServer
{
	// Token: 0x020000E3 RID: 227
	[MessageDescription("BattleServerManager", "BattleServer")]
	[Serializable]
	public class BattleReadyResponseMessage : FunctionResult
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00004DEA File Offset: 0x00002FEA
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00004DF2 File Offset: 0x00002FF2
		[JsonProperty]
		public bool ShouldReportActivities { get; private set; }

		// Token: 0x0600042E RID: 1070 RVA: 0x00004DFB File Offset: 0x00002FFB
		public BattleReadyResponseMessage()
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00004E03 File Offset: 0x00003003
		public BattleReadyResponseMessage(bool shouldReportActivities)
		{
			this.ShouldReportActivities = shouldReportActivities;
		}
	}
}
