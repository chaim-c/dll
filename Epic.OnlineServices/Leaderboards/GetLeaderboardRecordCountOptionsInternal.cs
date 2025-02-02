using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000408 RID: 1032
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardRecordCountOptionsInternal : ISettable<GetLeaderboardRecordCountOptions>, IDisposable
	{
		// Token: 0x06001AA2 RID: 6818 RVA: 0x000276D0 File Offset: 0x000258D0
		public void Set(ref GetLeaderboardRecordCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000276DC File Offset: 0x000258DC
		public void Set(ref GetLeaderboardRecordCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000276FD File Offset: 0x000258FD
		public void Dispose()
		{
		}

		// Token: 0x04000BD7 RID: 3031
		private int m_ApiVersion;
	}
}
