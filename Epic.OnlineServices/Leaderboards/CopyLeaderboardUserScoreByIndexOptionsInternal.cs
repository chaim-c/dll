using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000400 RID: 1024
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByIndexOptionsInternal : ISettable<CopyLeaderboardUserScoreByIndexOptions>, IDisposable
	{
		// Token: 0x1700077B RID: 1915
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0002728F File Offset: 0x0002548F
		public uint LeaderboardUserScoreIndex
		{
			set
			{
				this.m_LeaderboardUserScoreIndex = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00027299 File Offset: 0x00025499
		public Utf8String StatName
		{
			set
			{
				Helper.Set(value, ref this.m_StatName);
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000272A9 File Offset: 0x000254A9
		public void Set(ref CopyLeaderboardUserScoreByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LeaderboardUserScoreIndex = other.LeaderboardUserScoreIndex;
			this.StatName = other.StatName;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000272D0 File Offset: 0x000254D0
		public void Set(ref CopyLeaderboardUserScoreByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardUserScoreIndex = other.Value.LeaderboardUserScoreIndex;
				this.StatName = other.Value.StatName;
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0002731B File Offset: 0x0002551B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x04000BC3 RID: 3011
		private int m_ApiVersion;

		// Token: 0x04000BC4 RID: 3012
		private uint m_LeaderboardUserScoreIndex;

		// Token: 0x04000BC5 RID: 3013
		private IntPtr m_StatName;
	}
}
