using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000087 RID: 135
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : ISettable<GetFileMetadataCountOptions>, IDisposable
	{
		// Token: 0x170000D9 RID: 217
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00008231 File Offset: 0x00006431
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00008241 File Offset: 0x00006441
		public void Set(ref GetFileMetadataCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00008258 File Offset: 0x00006458
		public void Set(ref GetFileMetadataCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0000828E File Offset: 0x0000648E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040002A8 RID: 680
		private int m_ApiVersion;

		// Token: 0x040002A9 RID: 681
		private IntPtr m_LocalUserId;
	}
}
