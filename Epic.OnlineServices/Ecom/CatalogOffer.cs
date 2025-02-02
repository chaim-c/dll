using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000486 RID: 1158
	public struct CatalogOffer
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0002BE64 File Offset: 0x0002A064
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x0002BE6C File Offset: 0x0002A06C
		public int ServerIndex { get; set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x0002BE75 File Offset: 0x0002A075
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x0002BE7D File Offset: 0x0002A07D
		public Utf8String CatalogNamespace { get; set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0002BE86 File Offset: 0x0002A086
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0002BE8E File Offset: 0x0002A08E
		public Utf8String Id { get; set; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0002BE97 File Offset: 0x0002A097
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0002BE9F File Offset: 0x0002A09F
		public Utf8String TitleText { get; set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		// (set) Token: 0x06001DBD RID: 7613 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public Utf8String DescriptionText { get; set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0002BEB9 File Offset: 0x0002A0B9
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0002BEC1 File Offset: 0x0002A0C1
		public Utf8String LongDescriptionText { get; set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0002BECA File Offset: 0x0002A0CA
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0002BED2 File Offset: 0x0002A0D2
		public Utf8String TechnicalDetailsText_DEPRECATED { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0002BEDB File Offset: 0x0002A0DB
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x0002BEE3 File Offset: 0x0002A0E3
		public Utf8String CurrencyCode { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0002BEEC File Offset: 0x0002A0EC
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		public Result PriceResult { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0002BEFD File Offset: 0x0002A0FD
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x0002BF05 File Offset: 0x0002A105
		public uint OriginalPrice_DEPRECATED { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0002BF0E File Offset: 0x0002A10E
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x0002BF16 File Offset: 0x0002A116
		public uint CurrentPrice_DEPRECATED { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x0002BF1F File Offset: 0x0002A11F
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x0002BF27 File Offset: 0x0002A127
		public byte DiscountPercentage { get; set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0002BF30 File Offset: 0x0002A130
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0002BF38 File Offset: 0x0002A138
		public long ExpirationTimestamp { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x0002BF41 File Offset: 0x0002A141
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x0002BF49 File Offset: 0x0002A149
		public uint PurchasedCount_DEPRECATED { get; set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0002BF52 File Offset: 0x0002A152
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0002BF5A File Offset: 0x0002A15A
		public int PurchaseLimit { get; set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0002BF63 File Offset: 0x0002A163
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0002BF6B File Offset: 0x0002A16B
		public bool AvailableForPurchase { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0002BF74 File Offset: 0x0002A174
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0002BF7C File Offset: 0x0002A17C
		public ulong OriginalPrice64 { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0002BF85 File Offset: 0x0002A185
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0002BF8D File Offset: 0x0002A18D
		public ulong CurrentPrice64 { get; set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0002BF96 File Offset: 0x0002A196
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0002BF9E File Offset: 0x0002A19E
		public uint DecimalPoint { get; set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0002BFA7 File Offset: 0x0002A1A7
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x0002BFAF File Offset: 0x0002A1AF
		public long ReleaseDateTimestamp { get; set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
		public long EffectiveDateTimestamp { get; set; }

		// Token: 0x06001DDE RID: 7646 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		internal void Set(ref CatalogOfferInternal other)
		{
			this.ServerIndex = other.ServerIndex;
			this.CatalogNamespace = other.CatalogNamespace;
			this.Id = other.Id;
			this.TitleText = other.TitleText;
			this.DescriptionText = other.DescriptionText;
			this.LongDescriptionText = other.LongDescriptionText;
			this.TechnicalDetailsText_DEPRECATED = other.TechnicalDetailsText_DEPRECATED;
			this.CurrencyCode = other.CurrencyCode;
			this.PriceResult = other.PriceResult;
			this.OriginalPrice_DEPRECATED = other.OriginalPrice_DEPRECATED;
			this.CurrentPrice_DEPRECATED = other.CurrentPrice_DEPRECATED;
			this.DiscountPercentage = other.DiscountPercentage;
			this.ExpirationTimestamp = other.ExpirationTimestamp;
			this.PurchasedCount_DEPRECATED = other.PurchasedCount_DEPRECATED;
			this.PurchaseLimit = other.PurchaseLimit;
			this.AvailableForPurchase = other.AvailableForPurchase;
			this.OriginalPrice64 = other.OriginalPrice64;
			this.CurrentPrice64 = other.CurrentPrice64;
			this.DecimalPoint = other.DecimalPoint;
			this.ReleaseDateTimestamp = other.ReleaseDateTimestamp;
			this.EffectiveDateTimestamp = other.EffectiveDateTimestamp;
		}
	}
}
