using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000499 RID: 1177
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyItemImageInfoByIndexOptionsInternal : ISettable<CopyItemImageInfoByIndexOptions>, IDisposable
	{
		// Token: 0x170008CF RID: 2255
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x0002D181 File Offset: 0x0002B381
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0002D191 File Offset: 0x0002B391
		public Utf8String ItemId
		{
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0002D1A1 File Offset: 0x0002B3A1
		public uint ImageInfoIndex
		{
			set
			{
				this.m_ImageInfoIndex = value;
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0002D1AB File Offset: 0x0002B3AB
		public void Set(ref CopyItemImageInfoByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ItemId = other.ItemId;
			this.ImageInfoIndex = other.ImageInfoIndex;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		public void Set(ref CopyItemImageInfoByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ItemId = other.Value.ItemId;
				this.ImageInfoIndex = other.Value.ImageInfoIndex;
			}
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0002D23C File Offset: 0x0002B43C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ItemId);
		}

		// Token: 0x04000D79 RID: 3449
		private int m_ApiVersion;

		// Token: 0x04000D7A RID: 3450
		private IntPtr m_LocalUserId;

		// Token: 0x04000D7B RID: 3451
		private IntPtr m_ItemId;

		// Token: 0x04000D7C RID: 3452
		private uint m_ImageInfoIndex;
	}
}
