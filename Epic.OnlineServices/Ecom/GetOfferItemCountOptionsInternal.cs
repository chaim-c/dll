using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004BD RID: 1213
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferItemCountOptionsInternal : ISettable<GetOfferItemCountOptions>, IDisposable
	{
		// Token: 0x1700091C RID: 2332
		// (set) Token: 0x06001F4B RID: 8011 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0002EBC8 File Offset: 0x0002CDC8
		public Utf8String OfferId
		{
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		public void Set(ref GetOfferItemCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OfferId = other.OfferId;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0002EBFC File Offset: 0x0002CDFC
		public void Set(ref GetOfferItemCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferId = other.Value.OfferId;
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0002EC47 File Offset: 0x0002CE47
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x04000E0C RID: 3596
		private int m_ApiVersion;

		// Token: 0x04000E0D RID: 3597
		private IntPtr m_LocalUserId;

		// Token: 0x04000E0E RID: 3598
		private IntPtr m_OfferId;
	}
}
