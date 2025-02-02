using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C0 RID: 1216
	public struct ItemOwnership
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0002ECE1 File Offset: 0x0002CEE1
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x0002ECE9 File Offset: 0x0002CEE9
		public Utf8String Id { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0002ECF2 File Offset: 0x0002CEF2
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x0002ECFA File Offset: 0x0002CEFA
		public OwnershipStatus OwnershipStatus { get; set; }

		// Token: 0x06001F5A RID: 8026 RVA: 0x0002ED03 File Offset: 0x0002CF03
		internal void Set(ref ItemOwnershipInternal other)
		{
			this.Id = other.Id;
			this.OwnershipStatus = other.OwnershipStatus;
		}
	}
}
