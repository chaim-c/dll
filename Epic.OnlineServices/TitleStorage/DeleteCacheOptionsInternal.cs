using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000081 RID: 129
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheOptionsInternal : ISettable<DeleteCacheOptions>, IDisposable
	{
		// Token: 0x170000C4 RID: 196
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00007CDF File Offset: 0x00005EDF
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00007CEF File Offset: 0x00005EEF
		public void Set(ref DeleteCacheOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00007D08 File Offset: 0x00005F08
		public void Set(ref DeleteCacheOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00007D3E File Offset: 0x00005F3E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000292 RID: 658
		private int m_ApiVersion;

		// Token: 0x04000293 RID: 659
		private IntPtr m_LocalUserId;
	}
}
