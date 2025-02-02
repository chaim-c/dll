using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049F RID: 1183
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIdOptionsInternal : ISettable<CopyOfferByIdOptions>, IDisposable
	{
		// Token: 0x170008DE RID: 2270
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0002D440 File Offset: 0x0002B640
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0002D450 File Offset: 0x0002B650
		public Utf8String OfferId
		{
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x0002D460 File Offset: 0x0002B660
		public void Set(ref CopyOfferByIdOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.OfferId = other.OfferId;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0002D484 File Offset: 0x0002B684
		public void Set(ref CopyOfferByIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferId = other.Value.OfferId;
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x0002D4CF File Offset: 0x0002B6CF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x04000D8B RID: 3467
		private int m_ApiVersion;

		// Token: 0x04000D8C RID: 3468
		private IntPtr m_LocalUserId;

		// Token: 0x04000D8D RID: 3469
		private IntPtr m_OfferId;
	}
}
