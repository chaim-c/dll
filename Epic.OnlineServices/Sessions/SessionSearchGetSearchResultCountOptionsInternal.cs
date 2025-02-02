using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200014A RID: 330
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchGetSearchResultCountOptionsInternal : ISettable<SessionSearchGetSearchResultCountOptions>, IDisposable
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x0000DFE9 File Offset: 0x0000C1E9
		public void Set(ref SessionSearchGetSearchResultCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public void Set(ref SessionSearchGetSearchResultCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000E015 File Offset: 0x0000C215
		public void Dispose()
		{
		}

		// Token: 0x04000467 RID: 1127
		private int m_ApiVersion;
	}
}
