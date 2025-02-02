using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007B RID: 123
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : ISettable<CopyFileMetadataAtIndexOptions>, IDisposable
	{
		// Token: 0x170000B6 RID: 182
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00007999 File Offset: 0x00005B99
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x000079A9 File Offset: 0x00005BA9
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000079B3 File Offset: 0x00005BB3
		public void Set(ref CopyFileMetadataAtIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000079D8 File Offset: 0x00005BD8
		public void Set(ref CopyFileMetadataAtIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00007A23 File Offset: 0x00005C23
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000283 RID: 643
		private int m_ApiVersion;

		// Token: 0x04000284 RID: 644
		private IntPtr m_LocalUserId;

		// Token: 0x04000285 RID: 645
		private uint m_Index;
	}
}
