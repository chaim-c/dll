using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000F5 RID: 245
	[JsonConverter(typeof(BattlePlayerStatsBaseJsonConverter))]
	[Serializable]
	public class BattlePlayerStatsBase
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000563A File Offset: 0x0000383A
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00005642 File Offset: 0x00003842
		public string GameType { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000564B File Offset: 0x0000384B
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00005653 File Offset: 0x00003853
		public int Kills { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000565C File Offset: 0x0000385C
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00005664 File Offset: 0x00003864
		public int Assists { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000566D File Offset: 0x0000386D
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00005675 File Offset: 0x00003875
		public int Deaths { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000567E File Offset: 0x0000387E
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00005686 File Offset: 0x00003886
		public int PlayTime { get; set; }
	}
}
