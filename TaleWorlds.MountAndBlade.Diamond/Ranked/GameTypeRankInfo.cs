using System;

namespace TaleWorlds.MountAndBlade.Diamond.Ranked
{
	// Token: 0x02000166 RID: 358
	[Serializable]
	public class GameTypeRankInfo
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0000FC51 File Offset: 0x0000DE51
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0000FC59 File Offset: 0x0000DE59
		public string GameType { get; private set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0000FC62 File Offset: 0x0000DE62
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0000FC6A File Offset: 0x0000DE6A
		public RankBarInfo RankBarInfo { get; private set; }

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000FC73 File Offset: 0x0000DE73
		public GameTypeRankInfo(string gameType, RankBarInfo rankBarInfo)
		{
			this.GameType = gameType;
			this.RankBarInfo = rankBarInfo;
		}
	}
}
