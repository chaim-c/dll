using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public class NotEnoughPlayersInfo
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00007568 File Offset: 0x00005768
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00007570 File Offset: 0x00005770
		[JsonProperty]
		public int CurrentPlayerCount { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00007579 File Offset: 0x00005779
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00007581 File Offset: 0x00005781
		[JsonProperty]
		public int RequiredPlayerCount { get; private set; }

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000758A File Offset: 0x0000578A
		public NotEnoughPlayersInfo(int currentPlayerCount, int requiredPlayerCount)
		{
			this.CurrentPlayerCount = currentPlayerCount;
			this.RequiredPlayerCount = requiredPlayerCount;
		}
	}
}
