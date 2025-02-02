using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002EC RID: 748
	public struct CopyModInfoOptions
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0001DDD7 File Offset: 0x0001BFD7
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x0001DDDF File Offset: 0x0001BFDF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		public ModEnumerationType Type { get; set; }
	}
}
