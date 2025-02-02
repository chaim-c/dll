using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020F RID: 527
	public struct UserToken
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000160CF File Offset: 0x000142CF
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x000160D7 File Offset: 0x000142D7
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x000160E0 File Offset: 0x000142E0
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x000160E8 File Offset: 0x000142E8
		public Utf8String Token { get; set; }

		// Token: 0x06000EDC RID: 3804 RVA: 0x000160F1 File Offset: 0x000142F1
		internal void Set(ref UserTokenInternal other)
		{
			this.ProductUserId = other.ProductUserId;
			this.Token = other.Token;
		}
	}
}
