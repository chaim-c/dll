using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004BB RID: 1211
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferImageInfoCountOptionsInternal : ISettable<GetOfferImageInfoCountOptions>, IDisposable
	{
		// Token: 0x17000918 RID: 2328
		// (set) Token: 0x06001F42 RID: 8002 RVA: 0x0002EAEB File Offset: 0x0002CCEB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0002EAFB File Offset: 0x0002CCFB
		public Utf8String OfferId
		{
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0002EB0B File Offset: 0x0002CD0B
		public void Set(ref GetOfferImageInfoCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OfferId = other.OfferId;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0002EB30 File Offset: 0x0002CD30
		public void Set(ref GetOfferImageInfoCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferId = other.Value.OfferId;
			}
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0002EB7B File Offset: 0x0002CD7B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x04000E07 RID: 3591
		private int m_ApiVersion;

		// Token: 0x04000E08 RID: 3592
		private IntPtr m_LocalUserId;

		// Token: 0x04000E09 RID: 3593
		private IntPtr m_OfferId;
	}
}
