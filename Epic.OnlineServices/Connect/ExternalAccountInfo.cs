using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200052C RID: 1324
	public struct ExternalAccountInfo
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x00032BE0 File Offset: 0x00030DE0
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x00032BE8 File Offset: 0x00030DE8
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x00032BF1 File Offset: 0x00030DF1
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x00032BF9 File Offset: 0x00030DF9
		public Utf8String DisplayName { get; set; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00032C02 File Offset: 0x00030E02
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x00032C0A File Offset: 0x00030E0A
		public Utf8String AccountId { get; set; }

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00032C13 File Offset: 0x00030E13
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x00032C1B File Offset: 0x00030E1B
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x00032C24 File Offset: 0x00030E24
		// (set) Token: 0x060021FA RID: 8698 RVA: 0x00032C2C File Offset: 0x00030E2C
		public DateTimeOffset? LastLoginTime { get; set; }

		// Token: 0x060021FB RID: 8699 RVA: 0x00032C38 File Offset: 0x00030E38
		internal void Set(ref ExternalAccountInfoInternal other)
		{
			this.ProductUserId = other.ProductUserId;
			this.DisplayName = other.DisplayName;
			this.AccountId = other.AccountId;
			this.AccountIdType = other.AccountIdType;
			this.LastLoginTime = other.LastLoginTime;
		}
	}
}
