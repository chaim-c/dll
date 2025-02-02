using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000427 RID: 1063
	public struct CopyPermissionByIndexOptions
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x00028828 File Offset: 0x00026A28
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x00028830 File Offset: 0x00026A30
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00028839 File Offset: 0x00026A39
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x00028841 File Offset: 0x00026A41
		public uint Index { get; set; }
	}
}
