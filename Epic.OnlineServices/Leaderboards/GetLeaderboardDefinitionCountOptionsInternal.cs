using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000406 RID: 1030
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardDefinitionCountOptionsInternal : ISettable<GetLeaderboardDefinitionCountOptions>, IDisposable
	{
		// Token: 0x06001A9F RID: 6815 RVA: 0x0002769F File Offset: 0x0002589F
		public void Set(ref GetLeaderboardDefinitionCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000276AC File Offset: 0x000258AC
		public void Set(ref GetLeaderboardDefinitionCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000276CD File Offset: 0x000258CD
		public void Dispose()
		{
		}

		// Token: 0x04000BD6 RID: 3030
		private int m_ApiVersion;
	}
}
