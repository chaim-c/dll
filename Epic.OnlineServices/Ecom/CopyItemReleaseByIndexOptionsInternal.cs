using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200049B RID: 1179
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemReleaseByIndexOptionsInternal : ISettable<CopyItemReleaseByIndexOptions>, IDisposable
	{
		// Token: 0x170008D5 RID: 2261
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0002D28A File Offset: 0x0002B48A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x0002D29A File Offset: 0x0002B49A
		public Utf8String ItemId
		{
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x0002D2AA File Offset: 0x0002B4AA
		public uint ReleaseIndex
		{
			set
			{
				this.m_ReleaseIndex = value;
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0002D2B4 File Offset: 0x0002B4B4
		public void Set(ref CopyItemReleaseByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ItemId = other.ItemId;
			this.ReleaseIndex = other.ReleaseIndex;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		public void Set(ref CopyItemReleaseByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemId = other.Value.ItemId;
				this.ReleaseIndex = other.Value.ReleaseIndex;
			}
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0002D348 File Offset: 0x0002B548
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemId);
		}

		// Token: 0x04000D80 RID: 3456
		private int m_ApiVersion;

		// Token: 0x04000D81 RID: 3457
		private IntPtr m_LocalUserId;

		// Token: 0x04000D82 RID: 3458
		private IntPtr m_ItemId;

		// Token: 0x04000D83 RID: 3459
		private uint m_ReleaseIndex;
	}
}
