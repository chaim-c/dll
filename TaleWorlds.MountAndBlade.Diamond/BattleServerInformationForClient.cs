using System;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	public struct BattleServerInformationForClient
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00006DFD File Offset: 0x00004FFD
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00006E05 File Offset: 0x00005005
		public string MatchId { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00006E0E File Offset: 0x0000500E
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00006E16 File Offset: 0x00005016
		public string ServerAddress { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00006E1F File Offset: 0x0000501F
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00006E27 File Offset: 0x00005027
		public ushort ServerPort { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00006E30 File Offset: 0x00005030
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00006E38 File Offset: 0x00005038
		public int PeerIndex { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00006E41 File Offset: 0x00005041
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00006E49 File Offset: 0x00005049
		public int TeamNo { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00006E52 File Offset: 0x00005052
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00006E5A File Offset: 0x0000505A
		public int SessionKey { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00006E63 File Offset: 0x00005063
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00006E6B File Offset: 0x0000506B
		public string SceneName { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00006E74 File Offset: 0x00005074
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00006E7C File Offset: 0x0000507C
		public string GameType { get; set; }
	}
}
