using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004AD RID: 1197
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EntitlementInternal : IGettable<Entitlement>, ISettable<Entitlement>, IDisposable
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0002E48C File Offset: 0x0002C68C
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0002E4AD File Offset: 0x0002C6AD
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

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0002E4E1 File Offset: 0x0002C6E1
		public Utf8String EntitlementId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_EntitlementId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_EntitlementId);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0002E4F4 File Offset: 0x0002C6F4
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0002E515 File Offset: 0x0002C715
		public Utf8String CatalogItemId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CatalogItemId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CatalogItemId);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0002E528 File Offset: 0x0002C728
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0002E540 File Offset: 0x0002C740
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

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0002E54C File Offset: 0x0002C74C
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0002E56D File Offset: 0x0002C76D
		public bool Redeemed
		{
			get
			{
				bool result;
				Helper.Get(this.m_Redeemed, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Redeemed);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0002E580 File Offset: 0x0002C780
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x0002E598 File Offset: 0x0002C798
		public long EndTimestamp
		{
			get
			{
				return this.m_EndTimestamp;
			}
			set
			{
				this.m_EndTimestamp = value;
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		public void Set(ref Entitlement other)
		{
			this.m_ApiVersion = 2;
			this.EntitlementName = other.EntitlementName;
			this.EntitlementId = other.EntitlementId;
			this.CatalogItemId = other.CatalogItemId;
			this.ServerIndex = other.ServerIndex;
			this.Redeemed = other.Redeemed;
			this.EndTimestamp = other.EndTimestamp;
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0002E608 File Offset: 0x0002C808
		public void Set(ref Entitlement? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.EntitlementName = other.Value.EntitlementName;
				this.EntitlementId = other.Value.EntitlementId;
				this.CatalogItemId = other.Value.CatalogItemId;
				this.ServerIndex = other.Value.ServerIndex;
				this.Redeemed = other.Value.Redeemed;
				this.EndTimestamp = other.Value.EndTimestamp;
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0002E6AA File Offset: 0x0002C8AA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_EntitlementName);
			Helper.Dispose(ref this.m_EntitlementId);
			Helper.Dispose(ref this.m_CatalogItemId);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0002E6D1 File Offset: 0x0002C8D1
		public void Get(out Entitlement output)
		{
			output = default(Entitlement);
			output.Set(ref this);
		}

		// Token: 0x04000DE6 RID: 3558
		private int m_ApiVersion;

		// Token: 0x04000DE7 RID: 3559
		private IntPtr m_EntitlementName;

		// Token: 0x04000DE8 RID: 3560
		private IntPtr m_EntitlementId;

		// Token: 0x04000DE9 RID: 3561
		private IntPtr m_CatalogItemId;

		// Token: 0x04000DEA RID: 3562
		private int m_ServerIndex;

		// Token: 0x04000DEB RID: 3563
		private int m_Redeemed;

		// Token: 0x04000DEC RID: 3564
		private long m_EndTimestamp;
	}
}
