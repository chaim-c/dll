using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A3 RID: 1187
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferImageInfoByIndexOptionsInternal : ISettable<CopyOfferImageInfoByIndexOptions>, IDisposable
	{
		// Token: 0x170008E7 RID: 2279
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x0002D5D9 File Offset: 0x0002B7D9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0002D5E9 File Offset: 0x0002B7E9
		public Utf8String OfferId
		{
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x0002D5F9 File Offset: 0x0002B7F9
		public uint ImageInfoIndex
		{
			set
			{
				this.m_ImageInfoIndex = value;
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x0002D603 File Offset: 0x0002B803
		public void Set(ref CopyOfferImageInfoByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OfferId = other.OfferId;
			this.ImageInfoIndex = other.ImageInfoIndex;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x0002D634 File Offset: 0x0002B834
		public void Set(ref CopyOfferImageInfoByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferId = other.Value.OfferId;
				this.ImageInfoIndex = other.Value.ImageInfoIndex;
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0002D694 File Offset: 0x0002B894
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x04000D96 RID: 3478
		private int m_ApiVersion;

		// Token: 0x04000D97 RID: 3479
		private IntPtr m_LocalUserId;

		// Token: 0x04000D98 RID: 3480
		private IntPtr m_OfferId;

		// Token: 0x04000D99 RID: 3481
		private uint m_ImageInfoIndex;
	}
}
