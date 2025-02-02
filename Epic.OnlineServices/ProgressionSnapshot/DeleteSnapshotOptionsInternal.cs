using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000220 RID: 544
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteSnapshotOptionsInternal : ISettable<DeleteSnapshotOptions>, IDisposable
	{
		// Token: 0x170003FA RID: 1018
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x00016923 File Offset: 0x00014B23
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00016933 File Offset: 0x00014B33
		public void Set(ref DeleteSnapshotOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0001694C File Offset: 0x00014B4C
		public void Set(ref DeleteSnapshotOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00016982 File Offset: 0x00014B82
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040006D6 RID: 1750
		private int m_ApiVersion;

		// Token: 0x040006D7 RID: 1751
		private IntPtr m_LocalUserId;
	}
}
