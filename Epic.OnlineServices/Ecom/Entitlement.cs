using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004AC RID: 1196
	public struct Entitlement
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0002E3C7 File Offset: 0x0002C5C7
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0002E3CF File Offset: 0x0002C5CF
		public Utf8String EntitlementName { get; set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		public Utf8String EntitlementId { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0002E3E9 File Offset: 0x0002C5E9
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x0002E3F1 File Offset: 0x0002C5F1
		public Utf8String CatalogItemId { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0002E3FA File Offset: 0x0002C5FA
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x0002E402 File Offset: 0x0002C602
		public int ServerIndex { get; set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0002E40B File Offset: 0x0002C60B
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x0002E413 File Offset: 0x0002C613
		public bool Redeemed { get; set; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0002E41C File Offset: 0x0002C61C
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x0002E424 File Offset: 0x0002C624
		public long EndTimestamp { get; set; }

		// Token: 0x06001F00 RID: 7936 RVA: 0x0002E430 File Offset: 0x0002C630
		internal void Set(ref EntitlementInternal other)
		{
			this.EntitlementName = other.EntitlementName;
			this.EntitlementId = other.EntitlementId;
			this.CatalogItemId = other.CatalogItemId;
			this.ServerIndex = other.ServerIndex;
			this.Redeemed = other.Redeemed;
			this.EndTimestamp = other.EndTimestamp;
		}
	}
}
