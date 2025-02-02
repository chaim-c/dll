using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011F RID: 287
	public struct SessionDetailsAttribute
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0000C849 File Offset: 0x0000AA49
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0000C851 File Offset: 0x0000AA51
		public AttributeData? Data { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0000C85A File Offset: 0x0000AA5A
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0000C862 File Offset: 0x0000AA62
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000C86B File Offset: 0x0000AA6B
		internal void Set(ref SessionDetailsAttributeInternal other)
		{
			this.Data = other.Data;
			this.AdvertisementType = other.AdvertisementType;
		}
	}
}
