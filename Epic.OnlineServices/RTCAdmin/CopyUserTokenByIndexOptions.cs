using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001F8 RID: 504
	public struct CopyUserTokenByIndexOptions
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x000153DB File Offset: 0x000135DB
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x000153E3 File Offset: 0x000135E3
		public uint UserTokenIndex { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x000153EC File Offset: 0x000135EC
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x000153F4 File Offset: 0x000135F4
		public uint QueryId { get; set; }
	}
}
