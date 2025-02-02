using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000651 RID: 1617
	public struct InitializeThreadAffinity
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x0003DA46 File Offset: 0x0003BC46
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x0003DA4E File Offset: 0x0003BC4E
		public ulong NetworkWork { get; set; }

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x0003DA57 File Offset: 0x0003BC57
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x0003DA5F File Offset: 0x0003BC5F
		public ulong StorageIo { get; set; }

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x0003DA68 File Offset: 0x0003BC68
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x0003DA70 File Offset: 0x0003BC70
		public ulong WebSocketIo { get; set; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x0003DA79 File Offset: 0x0003BC79
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x0003DA81 File Offset: 0x0003BC81
		public ulong P2PIo { get; set; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0003DA8A File Offset: 0x0003BC8A
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x0003DA92 File Offset: 0x0003BC92
		public ulong HttpRequestIo { get; set; }

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x0003DA9B File Offset: 0x0003BC9B
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x0003DAA3 File Offset: 0x0003BCA3
		public ulong RTCIo { get; set; }

		// Token: 0x06002952 RID: 10578 RVA: 0x0003DAAC File Offset: 0x0003BCAC
		internal void Set(ref InitializeThreadAffinityInternal other)
		{
			this.NetworkWork = other.NetworkWork;
			this.StorageIo = other.StorageIo;
			this.WebSocketIo = other.WebSocketIo;
			this.P2PIo = other.P2PIo;
			this.HttpRequestIo = other.HttpRequestIo;
			this.RTCIo = other.RTCIo;
		}
	}
}
