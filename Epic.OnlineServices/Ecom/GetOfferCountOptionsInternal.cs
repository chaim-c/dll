using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B9 RID: 1209
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferCountOptionsInternal : ISettable<GetOfferCountOptions>, IDisposable
	{
		// Token: 0x17000915 RID: 2325
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0002EA5A File Offset: 0x0002CC5A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0002EA6A File Offset: 0x0002CC6A
		public void Set(ref GetOfferCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0002EA84 File Offset: 0x0002CC84
		public void Set(ref GetOfferCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0002EABA File Offset: 0x0002CCBA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000E03 RID: 3587
		private int m_ApiVersion;

		// Token: 0x04000E04 RID: 3588
		private IntPtr m_LocalUserId;
	}
}
