using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000133 RID: 307
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationAddAttributeOptionsInternal : ISettable<SessionModificationAddAttributeOptions>, IDisposable
	{
		// Token: 0x17000203 RID: 515
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0000D770 File Offset: 0x0000B970
		public AttributeData? SessionAttribute
		{
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_SessionAttribute);
			}
		}

		// Token: 0x17000204 RID: 516
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0000D781 File Offset: 0x0000B981
		public SessionAttributeAdvertisementType AdvertisementType
		{
			set
			{
				this.m_AdvertisementType = value;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000D78B File Offset: 0x0000B98B
		public void Set(ref SessionModificationAddAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionAttribute = other.SessionAttribute;
			this.AdvertisementType = other.AdvertisementType;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public void Set(ref SessionModificationAddAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionAttribute = other.Value.SessionAttribute;
				this.AdvertisementType = other.Value.AdvertisementType;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0000D7FB File Offset: 0x0000B9FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionAttribute);
		}

		// Token: 0x0400043D RID: 1085
		private int m_ApiVersion;

		// Token: 0x0400043E RID: 1086
		private IntPtr m_SessionAttribute;

		// Token: 0x0400043F RID: 1087
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
