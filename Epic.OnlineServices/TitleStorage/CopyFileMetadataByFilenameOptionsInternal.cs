using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007D RID: 125
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : ISettable<CopyFileMetadataByFilenameOptions>, IDisposable
	{
		// Token: 0x170000BA RID: 186
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00007A54 File Offset: 0x00005C54
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000BB RID: 187
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x00007A64 File Offset: 0x00005C64
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00007A74 File Offset: 0x00005C74
		public void Set(ref CopyFileMetadataByFilenameOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00007A98 File Offset: 0x00005C98
		public void Set(ref CopyFileMetadataByFilenameOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00007AE3 File Offset: 0x00005CE3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x04000288 RID: 648
		private int m_ApiVersion;

		// Token: 0x04000289 RID: 649
		private IntPtr m_LocalUserId;

		// Token: 0x0400028A RID: 650
		private IntPtr m_Filename;
	}
}
