using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F0 RID: 752
	public struct EnumerateModsOptions
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0001E0C7 File Offset: 0x0001C2C7
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0001E0CF File Offset: 0x0001C2CF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		public ModEnumerationType Type { get; set; }
	}
}
