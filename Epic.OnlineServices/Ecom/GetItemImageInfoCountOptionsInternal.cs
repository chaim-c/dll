using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004B3 RID: 1203
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetItemImageInfoCountOptionsInternal : ISettable<GetItemImageInfoCountOptions>, IDisposable
	{
		// Token: 0x1700090C RID: 2316
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x0002E853 File Offset: 0x0002CA53
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x0002E863 File Offset: 0x0002CA63
		public Utf8String ItemId
		{
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0002E873 File Offset: 0x0002CA73
		public void Set(ref GetItemImageInfoCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ItemId = other.ItemId;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0002E898 File Offset: 0x0002CA98
		public void Set(ref GetItemImageInfoCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemId = other.Value.ItemId;
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0002E8E3 File Offset: 0x0002CAE3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemId);
		}

		// Token: 0x04000DF7 RID: 3575
		private int m_ApiVersion;

		// Token: 0x04000DF8 RID: 3576
		private IntPtr m_LocalUserId;

		// Token: 0x04000DF9 RID: 3577
		private IntPtr m_ItemId;
	}
}
