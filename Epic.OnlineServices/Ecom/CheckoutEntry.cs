using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048C RID: 1164
	public struct CheckoutEntry
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x0002CC07 File Offset: 0x0002AE07
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0002CC0F File Offset: 0x0002AE0F
		public Utf8String OfferId { get; set; }

		// Token: 0x06001E37 RID: 7735 RVA: 0x0002CC18 File Offset: 0x0002AE18
		internal void Set(ref CheckoutEntryInternal other)
		{
			this.OfferId = other.OfferId;
		}
	}
}
