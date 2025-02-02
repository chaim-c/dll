using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public class FriendInfo
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000834C File Offset: 0x0000654C
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00008354 File Offset: 0x00006554
		public PlayerId Id { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000835D File Offset: 0x0000655D
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00008365 File Offset: 0x00006565
		public FriendStatus Status { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000836E File Offset: 0x0000656E
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00008376 File Offset: 0x00006576
		public string Name { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000837F File Offset: 0x0000657F
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x00008387 File Offset: 0x00006587
		public bool IsOnline { get; set; }
	}
}
