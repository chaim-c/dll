using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A1 RID: 1185
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferByIndexOptionsInternal : ISettable<CopyOfferByIndexOptions>, IDisposable
	{
		// Token: 0x170008E2 RID: 2274
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x0002D50C File Offset: 0x0002B70C
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0002D51C File Offset: 0x0002B71C
		public uint OfferIndex
		{
			set
			{
				this.m_OfferIndex = value;
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0002D526 File Offset: 0x0002B726
		public void Set(ref CopyOfferByIndexOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.OfferIndex = other.OfferIndex;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0002D54C File Offset: 0x0002B74C
		public void Set(ref CopyOfferByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferIndex = other.Value.OfferIndex;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0002D597 File Offset: 0x0002B797
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000D90 RID: 3472
		private int m_ApiVersion;

		// Token: 0x04000D91 RID: 3473
		private IntPtr m_LocalUserId;

		// Token: 0x04000D92 RID: 3474
		private uint m_OfferIndex;
	}
}
