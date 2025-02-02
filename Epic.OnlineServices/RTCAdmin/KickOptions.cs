using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FE RID: 510
	public struct KickOptions
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00015695 File Offset: 0x00013895
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0001569D File Offset: 0x0001389D
		public Utf8String RoomName { get; set; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000156A6 File Offset: 0x000138A6
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x000156AE File Offset: 0x000138AE
		public ProductUserId TargetUserId { get; set; }
	}
}
