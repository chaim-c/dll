using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000262 RID: 610
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : ISettable<CopyFileMetadataByFilenameOptions>, IDisposable
	{
		// Token: 0x17000466 RID: 1126
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00018B20 File Offset: 0x00016D20
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00018B30 File Offset: 0x00016D30
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00018B40 File Offset: 0x00016D40
		public void Set(ref CopyFileMetadataByFilenameOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00018B64 File Offset: 0x00016D64
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

		// Token: 0x060010AD RID: 4269 RVA: 0x00018BAF File Offset: 0x00016DAF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0400077A RID: 1914
		private int m_ApiVersion;

		// Token: 0x0400077B RID: 1915
		private IntPtr m_LocalUserId;

		// Token: 0x0400077C RID: 1916
		private IntPtr m_Filename;
	}
}
