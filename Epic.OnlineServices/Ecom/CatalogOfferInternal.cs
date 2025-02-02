using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000487 RID: 1159
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogOfferInternal : IGettable<CatalogOffer>, ISettable<CatalogOffer>, IDisposable
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0002C0EC File Offset: 0x0002A2EC
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0002C104 File Offset: 0x0002A304
		public int ServerIndex
		{
			get
			{
				return this.m_ServerIndex;
			}
			set
			{
				this.m_ServerIndex = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0002C110 File Offset: 0x0002A310
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0002C131 File Offset: 0x0002A331
		public Utf8String CatalogNamespace
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CatalogNamespace, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CatalogNamespace);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0002C144 File Offset: 0x0002A344
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0002C165 File Offset: 0x0002A365
		public Utf8String Id
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Id);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0002C178 File Offset: 0x0002A378
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0002C199 File Offset: 0x0002A399
		public Utf8String TitleText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TitleText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TitleText);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x0002C1CD File Offset: 0x0002A3CD
		public Utf8String DescriptionText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DescriptionText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DescriptionText);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x0002C201 File Offset: 0x0002A401
		public Utf8String LongDescriptionText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LongDescriptionText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LongDescriptionText);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x0002C214 File Offset: 0x0002A414
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x0002C235 File Offset: 0x0002A435
		public Utf8String TechnicalDetailsText_DEPRECATED
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TechnicalDetailsText_DEPRECATED, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TechnicalDetailsText_DEPRECATED);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x0002C248 File Offset: 0x0002A448
		// (set) Token: 0x06001DEE RID: 7662 RVA: 0x0002C269 File Offset: 0x0002A469
		public Utf8String CurrencyCode
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CurrencyCode, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CurrencyCode);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x0002C27C File Offset: 0x0002A47C
		// (set) Token: 0x06001DF0 RID: 7664 RVA: 0x0002C294 File Offset: 0x0002A494
		public Result PriceResult
		{
			get
			{
				return this.m_PriceResult;
			}
			set
			{
				this.m_PriceResult = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0002C2A0 File Offset: 0x0002A4A0
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		public uint OriginalPrice_DEPRECATED
		{
			get
			{
				return this.m_OriginalPrice_DEPRECATED;
			}
			set
			{
				this.m_OriginalPrice_DEPRECATED = value;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0002C2C4 File Offset: 0x0002A4C4
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0002C2DC File Offset: 0x0002A4DC
		public uint CurrentPrice_DEPRECATED
		{
			get
			{
				return this.m_CurrentPrice_DEPRECATED;
			}
			set
			{
				this.m_CurrentPrice_DEPRECATED = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0002C2E8 File Offset: 0x0002A4E8
		// (set) Token: 0x06001DF6 RID: 7670 RVA: 0x0002C300 File Offset: 0x0002A500
		public byte DiscountPercentage
		{
			get
			{
				return this.m_DiscountPercentage;
			}
			set
			{
				this.m_DiscountPercentage = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0002C30C File Offset: 0x0002A50C
		// (set) Token: 0x06001DF8 RID: 7672 RVA: 0x0002C324 File Offset: 0x0002A524
		public long ExpirationTimestamp
		{
			get
			{
				return this.m_ExpirationTimestamp;
			}
			set
			{
				this.m_ExpirationTimestamp = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0002C330 File Offset: 0x0002A530
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0002C348 File Offset: 0x0002A548
		public uint PurchasedCount_DEPRECATED
		{
			get
			{
				return this.m_PurchasedCount_DEPRECATED;
			}
			set
			{
				this.m_PurchasedCount_DEPRECATED = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0002C354 File Offset: 0x0002A554
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0002C36C File Offset: 0x0002A56C
		public int PurchaseLimit
		{
			get
			{
				return this.m_PurchaseLimit;
			}
			set
			{
				this.m_PurchaseLimit = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0002C378 File Offset: 0x0002A578
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x0002C399 File Offset: 0x0002A599
		public bool AvailableForPurchase
		{
			get
			{
				bool result;
				Helper.Get(this.m_AvailableForPurchase, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AvailableForPurchase);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x0002C3C4 File Offset: 0x0002A5C4
		public ulong OriginalPrice64
		{
			get
			{
				return this.m_OriginalPrice64;
			}
			set
			{
				this.m_OriginalPrice64 = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x0002C3E8 File Offset: 0x0002A5E8
		public ulong CurrentPrice64
		{
			get
			{
				return this.m_CurrentPrice64;
			}
			set
			{
				this.m_CurrentPrice64 = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x0002C40C File Offset: 0x0002A60C
		public uint DecimalPoint
		{
			get
			{
				return this.m_DecimalPoint;
			}
			set
			{
				this.m_DecimalPoint = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0002C418 File Offset: 0x0002A618
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0002C430 File Offset: 0x0002A630
		public long ReleaseDateTimestamp
		{
			get
			{
				return this.m_ReleaseDateTimestamp;
			}
			set
			{
				this.m_ReleaseDateTimestamp = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0002C43C File Offset: 0x0002A63C
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0002C454 File Offset: 0x0002A654
		public long EffectiveDateTimestamp
		{
			get
			{
				return this.m_EffectiveDateTimestamp;
			}
			set
			{
				this.m_EffectiveDateTimestamp = value;
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0002C460 File Offset: 0x0002A660
		public void Set(ref CatalogOffer other)
		{
			this.m_ApiVersion = 5;
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

		// Token: 0x06001E0A RID: 7690 RVA: 0x0002C588 File Offset: 0x0002A788
		public void Set(ref CatalogOffer? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 5;
				this.ServerIndex = other.Value.ServerIndex;
				this.CatalogNamespace = other.Value.CatalogNamespace;
				this.Id = other.Value.Id;
				this.TitleText = other.Value.TitleText;
				this.DescriptionText = other.Value.DescriptionText;
				this.LongDescriptionText = other.Value.LongDescriptionText;
				this.TechnicalDetailsText_DEPRECATED = other.Value.TechnicalDetailsText_DEPRECATED;
				this.CurrencyCode = other.Value.CurrencyCode;
				this.PriceResult = other.Value.PriceResult;
				this.OriginalPrice_DEPRECATED = other.Value.OriginalPrice_DEPRECATED;
				this.CurrentPrice_DEPRECATED = other.Value.CurrentPrice_DEPRECATED;
				this.DiscountPercentage = other.Value.DiscountPercentage;
				this.ExpirationTimestamp = other.Value.ExpirationTimestamp;
				this.PurchasedCount_DEPRECATED = other.Value.PurchasedCount_DEPRECATED;
				this.PurchaseLimit = other.Value.PurchaseLimit;
				this.AvailableForPurchase = other.Value.AvailableForPurchase;
				this.OriginalPrice64 = other.Value.OriginalPrice64;
				this.CurrentPrice64 = other.Value.CurrentPrice64;
				this.DecimalPoint = other.Value.DecimalPoint;
				this.ReleaseDateTimestamp = other.Value.ReleaseDateTimestamp;
				this.EffectiveDateTimestamp = other.Value.EffectiveDateTimestamp;
			}
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0002C768 File Offset: 0x0002A968
		public void Dispose()
		{
			Helper.Dispose(ref this.m_CatalogNamespace);
			Helper.Dispose(ref this.m_Id);
			Helper.Dispose(ref this.m_TitleText);
			Helper.Dispose(ref this.m_DescriptionText);
			Helper.Dispose(ref this.m_LongDescriptionText);
			Helper.Dispose(ref this.m_TechnicalDetailsText_DEPRECATED);
			Helper.Dispose(ref this.m_CurrencyCode);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0002C7CA File Offset: 0x0002A9CA
		public void Get(out CatalogOffer output)
		{
			output = default(CatalogOffer);
			output.Set(ref this);
		}

		// Token: 0x04000D2E RID: 3374
		private int m_ApiVersion;

		// Token: 0x04000D2F RID: 3375
		private int m_ServerIndex;

		// Token: 0x04000D30 RID: 3376
		private IntPtr m_CatalogNamespace;

		// Token: 0x04000D31 RID: 3377
		private IntPtr m_Id;

		// Token: 0x04000D32 RID: 3378
		private IntPtr m_TitleText;

		// Token: 0x04000D33 RID: 3379
		private IntPtr m_DescriptionText;

		// Token: 0x04000D34 RID: 3380
		private IntPtr m_LongDescriptionText;

		// Token: 0x04000D35 RID: 3381
		private IntPtr m_TechnicalDetailsText_DEPRECATED;

		// Token: 0x04000D36 RID: 3382
		private IntPtr m_CurrencyCode;

		// Token: 0x04000D37 RID: 3383
		private Result m_PriceResult;

		// Token: 0x04000D38 RID: 3384
		private uint m_OriginalPrice_DEPRECATED;

		// Token: 0x04000D39 RID: 3385
		private uint m_CurrentPrice_DEPRECATED;

		// Token: 0x04000D3A RID: 3386
		private byte m_DiscountPercentage;

		// Token: 0x04000D3B RID: 3387
		private long m_ExpirationTimestamp;

		// Token: 0x04000D3C RID: 3388
		private uint m_PurchasedCount_DEPRECATED;

		// Token: 0x04000D3D RID: 3389
		private int m_PurchaseLimit;

		// Token: 0x04000D3E RID: 3390
		private int m_AvailableForPurchase;

		// Token: 0x04000D3F RID: 3391
		private ulong m_OriginalPrice64;

		// Token: 0x04000D40 RID: 3392
		private ulong m_CurrentPrice64;

		// Token: 0x04000D41 RID: 3393
		private uint m_DecimalPoint;

		// Token: 0x04000D42 RID: 3394
		private long m_ReleaseDateTimestamp;

		// Token: 0x04000D43 RID: 3395
		private long m_EffectiveDateTimestamp;
	}
}
