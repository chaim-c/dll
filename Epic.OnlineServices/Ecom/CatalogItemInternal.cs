using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000485 RID: 1157
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogItemInternal : IGettable<CatalogItem>, ISettable<CatalogItem>, IDisposable
	{
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001D9C RID: 7580 RVA: 0x0002BA6C File Offset: 0x00029C6C
		// (set) Token: 0x06001D9D RID: 7581 RVA: 0x0002BA8D File Offset: 0x00029C8D
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001D9E RID: 7582 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		// (set) Token: 0x06001D9F RID: 7583 RVA: 0x0002BAC1 File Offset: 0x00029CC1
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

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001DA0 RID: 7584 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		// (set) Token: 0x06001DA1 RID: 7585 RVA: 0x0002BAF5 File Offset: 0x00029CF5
		public Utf8String EntitlementName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_EntitlementName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_EntitlementName);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0002BB08 File Offset: 0x00029D08
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0002BB29 File Offset: 0x00029D29
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

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0002BB3C File Offset: 0x00029D3C
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0002BB5D File Offset: 0x00029D5D
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

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0002BB70 File Offset: 0x00029D70
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0002BB91 File Offset: 0x00029D91
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

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0002BBA4 File Offset: 0x00029DA4
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x0002BBC5 File Offset: 0x00029DC5
		public Utf8String TechnicalDetailsText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TechnicalDetailsText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TechnicalDetailsText);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0002BBF9 File Offset: 0x00029DF9
		public Utf8String DeveloperText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DeveloperText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DeveloperText);
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0002BC0C File Offset: 0x00029E0C
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0002BC24 File Offset: 0x00029E24
		public EcomItemType ItemType
		{
			get
			{
				return this.m_ItemType;
			}
			set
			{
				this.m_ItemType = value;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0002BC30 File Offset: 0x00029E30
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0002BC48 File Offset: 0x00029E48
		public long EntitlementEndTimestamp
		{
			get
			{
				return this.m_EntitlementEndTimestamp;
			}
			set
			{
				this.m_EntitlementEndTimestamp = value;
			}
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0002BC54 File Offset: 0x00029E54
		public void Set(ref CatalogItem other)
		{
			this.m_ApiVersion = 1;
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

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0002BCEC File Offset: 0x00029EEC
		public void Set(ref CatalogItem? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.CatalogNamespace = other.Value.CatalogNamespace;
				this.Id = other.Value.Id;
				this.EntitlementName = other.Value.EntitlementName;
				this.TitleText = other.Value.TitleText;
				this.DescriptionText = other.Value.DescriptionText;
				this.LongDescriptionText = other.Value.LongDescriptionText;
				this.TechnicalDetailsText = other.Value.TechnicalDetailsText;
				this.DeveloperText = other.Value.DeveloperText;
				this.ItemType = other.Value.ItemType;
				this.EntitlementEndTimestamp = other.Value.EntitlementEndTimestamp;
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_CatalogNamespace);
			Helper.Dispose(ref this.m_Id);
			Helper.Dispose(ref this.m_EntitlementName);
			Helper.Dispose(ref this.m_TitleText);
			Helper.Dispose(ref this.m_DescriptionText);
			Helper.Dispose(ref this.m_LongDescriptionText);
			Helper.Dispose(ref this.m_TechnicalDetailsText);
			Helper.Dispose(ref this.m_DeveloperText);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0002BE52 File Offset: 0x0002A052
		public void Get(out CatalogItem output)
		{
			output = default(CatalogItem);
			output.Set(ref this);
		}

		// Token: 0x04000D0E RID: 3342
		private int m_ApiVersion;

		// Token: 0x04000D0F RID: 3343
		private IntPtr m_CatalogNamespace;

		// Token: 0x04000D10 RID: 3344
		private IntPtr m_Id;

		// Token: 0x04000D11 RID: 3345
		private IntPtr m_EntitlementName;

		// Token: 0x04000D12 RID: 3346
		private IntPtr m_TitleText;

		// Token: 0x04000D13 RID: 3347
		private IntPtr m_DescriptionText;

		// Token: 0x04000D14 RID: 3348
		private IntPtr m_LongDescriptionText;

		// Token: 0x04000D15 RID: 3349
		private IntPtr m_TechnicalDetailsText;

		// Token: 0x04000D16 RID: 3350
		private IntPtr m_DeveloperText;

		// Token: 0x04000D17 RID: 3351
		private EcomItemType m_ItemType;

		// Token: 0x04000D18 RID: 3352
		private long m_EntitlementEndTimestamp;
	}
}
