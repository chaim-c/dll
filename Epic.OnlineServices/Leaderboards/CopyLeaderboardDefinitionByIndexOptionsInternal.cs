using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003F8 RID: 1016
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByIndexOptionsInternal : ISettable<CopyLeaderboardDefinitionByIndexOptions>, IDisposable
	{
		// Token: 0x17000772 RID: 1906
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x000270A6 File Offset: 0x000252A6
		public uint LeaderboardIndex
		{
			set
			{
				this.m_LeaderboardIndex = value;
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000270B0 File Offset: 0x000252B0
		public void Set(ref CopyLeaderboardDefinitionByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LeaderboardIndex = other.LeaderboardIndex;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000270C8 File Offset: 0x000252C8
		public void Set(ref CopyLeaderboardDefinitionByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardIndex = other.Value.LeaderboardIndex;
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000270FE File Offset: 0x000252FE
		public void Dispose()
		{
		}

		// Token: 0x04000BB6 RID: 2998
		private int m_ApiVersion;

		// Token: 0x04000BB7 RID: 2999
		private uint m_LeaderboardIndex;
	}
}
