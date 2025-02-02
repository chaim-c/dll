using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class BattlePlayerEntry
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00005588 File Offset: 0x00003788
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00005590 File Offset: 0x00003790
		public PlayerId PlayerId { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00005599 File Offset: 0x00003799
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000055A1 File Offset: 0x000037A1
		public int TeamNo { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000055AA File Offset: 0x000037AA
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000055B2 File Offset: 0x000037B2
		public Guid Party { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000055BB File Offset: 0x000037BB
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000055C3 File Offset: 0x000037C3
		public BattlePlayerStatsBase PlayerStats { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000055CC File Offset: 0x000037CC
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000055D4 File Offset: 0x000037D4
		public int PlayTime { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000055DD File Offset: 0x000037DD
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x000055E5 File Offset: 0x000037E5
		public DateTime LastJoinTime { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000055EE File Offset: 0x000037EE
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x000055F6 File Offset: 0x000037F6
		public bool Disconnected { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x000055FF File Offset: 0x000037FF
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00005607 File Offset: 0x00003807
		public string GameType { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00005610 File Offset: 0x00003810
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00005618 File Offset: 0x00003818
		public bool Won { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00005621 File Offset: 0x00003821
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00005629 File Offset: 0x00003829
		public BattleJoinType BattleJoinType { get; set; }
	}
}
