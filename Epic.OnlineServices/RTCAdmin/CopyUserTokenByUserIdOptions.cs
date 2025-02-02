using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FA RID: 506
	public struct CopyUserTokenByUserIdOptions
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00015486 File Offset: 0x00013686
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0001548E File Offset: 0x0001368E
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00015497 File Offset: 0x00013697
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x0001549F File Offset: 0x0001369F
		public uint QueryId { get; set; }
	}
}
