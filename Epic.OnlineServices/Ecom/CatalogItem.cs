using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000484 RID: 1156
	public struct CatalogItem
	{
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x0002B932 File Offset: 0x00029B32
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0002B93A File Offset: 0x00029B3A
		public Utf8String CatalogNamespace { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0002B943 File Offset: 0x00029B43
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0002B94B File Offset: 0x00029B4B
		public Utf8String Id { get; set; }

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0002B954 File Offset: 0x00029B54
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0002B95C File Offset: 0x00029B5C
		public Utf8String EntitlementName { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x0002B965 File Offset: 0x00029B65
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x0002B96D File Offset: 0x00029B6D
		public Utf8String TitleText { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x0002B976 File Offset: 0x00029B76
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x0002B97E File Offset: 0x00029B7E
		public Utf8String DescriptionText { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0002B987 File Offset: 0x00029B87
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0002B98F File Offset: 0x00029B8F
		public Utf8String LongDescriptionText { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0002B998 File Offset: 0x00029B98
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0002B9A0 File Offset: 0x00029BA0
		public Utf8String TechnicalDetailsText { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0002B9A9 File Offset: 0x00029BA9
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0002B9B1 File Offset: 0x00029BB1
		public Utf8String DeveloperText { get; set; }

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0002B9BA File Offset: 0x00029BBA
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0002B9C2 File Offset: 0x00029BC2
		public EcomItemType ItemType { get; set; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0002B9CB File Offset: 0x00029BCB
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x0002B9D3 File Offset: 0x00029BD3
		public long EntitlementEndTimestamp { get; set; }

		// Token: 0x06001D9B RID: 7579 RVA: 0x0002B9DC File Offset: 0x00029BDC
		internal void Set(ref CatalogItemInternal other)
		{
			this.CatalogNamespace = other.CatalogNamespace;
			this.Id = other.Id;
			this.EntitlementName = other.EntitlementName;
			this.TitleText = other.TitleText;
			this.DescriptionText = other.DescriptionText;
			this.LongDescriptionText = other.LongDescriptionText;
			this.TechnicalDetailsText = other.TechnicalDetailsText;
			this.DeveloperText = other.DeveloperText;
			this.ItemType = other.ItemType;
			this.EntitlementEndTimestamp = other.EntitlementEndTimestamp;
		}
	}
}
