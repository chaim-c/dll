using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200051A RID: 1306
	public struct CopyProductUserExternalAccountByIndexOptions
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x000323DA File Offset: 0x000305DA
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x000323E2 File Offset: 0x000305E2
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x000323EB File Offset: 0x000305EB
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x000323F3 File Offset: 0x000305F3
		public uint ExternalAccountInfoIndex { get; set; }
	}
}
