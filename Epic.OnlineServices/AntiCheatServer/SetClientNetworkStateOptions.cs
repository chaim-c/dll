using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005D0 RID: 1488
	public struct SetClientNetworkStateOptions
	{
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00038D6D File Offset: 0x00036F6D
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x00038D75 File Offset: 0x00036F75
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x00038D7E File Offset: 0x00036F7E
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x00038D86 File Offset: 0x00036F86
		public bool IsNetworkActive { get; set; }
	}
}
