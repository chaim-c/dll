using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B5 RID: 1205
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemReleaseCountOptionsInternal : ISettable<GetItemReleaseCountOptions>, IDisposable
	{
		// Token: 0x17000910 RID: 2320
		// (set) Token: 0x06001F2D RID: 7981 RVA: 0x0002E920 File Offset: 0x0002CB20
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x0002E930 File Offset: 0x0002CB30
		public Utf8String ItemId
		{
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0002E940 File Offset: 0x0002CB40
		public void Set(ref GetItemReleaseCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ItemId = other.ItemId;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0002E964 File Offset: 0x0002CB64
		public void Set(ref GetItemReleaseCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemId = other.Value.ItemId;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0002E9AF File Offset: 0x0002CBAF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemId);
		}

		// Token: 0x04000DFC RID: 3580
		private int m_ApiVersion;

		// Token: 0x04000DFD RID: 3581
		private IntPtr m_LocalUserId;

		// Token: 0x04000DFE RID: 3582
		private IntPtr m_ItemId;
	}
}
