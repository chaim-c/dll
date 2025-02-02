using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public class BattlePlayerStatsSkirmish : BattlePlayerStatsBase
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x000058A7 File Offset: 0x00003AA7
		public BattlePlayerStatsSkirmish()
		{
			base.GameType = "Skirmish";
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000058BA File Offset: 0x00003ABA
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x000058C2 File Offset: 0x00003AC2
		public int MVPs { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000058CB File Offset: 0x00003ACB
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x000058D3 File Offset: 0x00003AD3
		public int Score { get; set; }
	}
}
