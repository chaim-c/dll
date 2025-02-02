using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000120 RID: 288
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsAttributeInternal : IGettable<SessionDetailsAttribute>, ISettable<SessionDetailsAttribute>, IDisposable
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0000C888 File Offset: 0x0000AA88
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0000C8A9 File Offset: 0x0000AAA9
		public AttributeData? Data
		{
			get
			{
				AttributeData? result;
				Helper.Get<AttributeDataInternal, AttributeData>(this.m_Data, out result);
				return result;
			}
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Data);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0000C8BC File Offset: 0x0000AABC
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public SessionAttributeAdvertisementType AdvertisementType
		{
			get
			{
				return this.m_AdvertisementType;
			}
			set
			{
				this.m_AdvertisementType = value;
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000C8DE File Offset: 0x0000AADE
		public void Set(ref SessionDetailsAttribute other)
		{
			this.m_ApiVersion = 1;
			this.Data = other.Data;
			this.AdvertisementType = other.AdvertisementType;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000C904 File Offset: 0x0000AB04
		public void Set(ref SessionDetailsAttribute? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Value.Data;
				this.AdvertisementType = other.Value.AdvertisementType;
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000C94F File Offset: 0x0000AB4F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000C95E File Offset: 0x0000AB5E
		public void Get(out SessionDetailsAttribute output)
		{
			output = default(SessionDetailsAttribute);
			output.Set(ref this);
		}

		// Token: 0x040003FC RID: 1020
		private int m_ApiVersion;

		// Token: 0x040003FD RID: 1021
		private IntPtr m_Data;

		// Token: 0x040003FE RID: 1022
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
