using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026E RID: 622
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileOptionsInternal : ISettable<DuplicateFileOptions>, IDisposable
	{
		// Token: 0x17000486 RID: 1158
		// (set) Token: 0x060010FC RID: 4348 RVA: 0x000192B5 File Offset: 0x000174B5
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x000192C5 File Offset: 0x000174C5
		public Utf8String SourceFilename
		{
			set
			{
				Helper.Set(value, ref this.m_SourceFilename);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x000192D5 File Offset: 0x000174D5
		public Utf8String DestinationFilename
		{
			set
			{
				Helper.Set(value, ref this.m_DestinationFilename);
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000192E5 File Offset: 0x000174E5
		public void Set(ref DuplicateFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.SourceFilename = other.SourceFilename;
			this.DestinationFilename = other.DestinationFilename;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00019318 File Offset: 0x00017518
		public void Set(ref DuplicateFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.SourceFilename = other.Value.SourceFilename;
				this.DestinationFilename = other.Value.DestinationFilename;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00019378 File Offset: 0x00017578
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SourceFilename);
			Helper.Dispose(ref this.m_DestinationFilename);
		}

		// Token: 0x0400079A RID: 1946
		private int m_ApiVersion;

		// Token: 0x0400079B RID: 1947
		private IntPtr m_LocalUserId;

		// Token: 0x0400079C RID: 1948
		private IntPtr m_SourceFilename;

		// Token: 0x0400079D RID: 1949
		private IntPtr m_DestinationFilename;
	}
}
