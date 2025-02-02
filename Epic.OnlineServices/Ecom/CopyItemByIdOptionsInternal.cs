using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000497 RID: 1175
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemByIdOptionsInternal : ISettable<CopyItemByIdOptions>, IDisposable
	{
		// Token: 0x170008CA RID: 2250
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0002D0A1 File Offset: 0x0002B2A1
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0002D0B1 File Offset: 0x0002B2B1
		public Utf8String ItemId
		{
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0002D0C1 File Offset: 0x0002B2C1
		public void Set(ref CopyItemByIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ItemId = other.ItemId;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0002D0E8 File Offset: 0x0002B2E8
		public void Set(ref CopyItemByIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemId = other.Value.ItemId;
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0002D133 File Offset: 0x0002B333
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemId);
		}

		// Token: 0x04000D73 RID: 3443
		private int m_ApiVersion;

		// Token: 0x04000D74 RID: 3444
		private IntPtr m_LocalUserId;

		// Token: 0x04000D75 RID: 3445
		private IntPtr m_ItemId;
	}
}
