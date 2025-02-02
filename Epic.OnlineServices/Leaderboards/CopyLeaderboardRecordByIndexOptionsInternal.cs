using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003FC RID: 1020
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByIndexOptionsInternal : ISettable<CopyLeaderboardRecordByIndexOptions>, IDisposable
	{
		// Token: 0x17000776 RID: 1910
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00027192 File Offset: 0x00025392
		public uint LeaderboardRecordIndex
		{
			set
			{
				this.m_LeaderboardRecordIndex = value;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0002719C File Offset: 0x0002539C
		public void Set(ref CopyLeaderboardRecordByIndexOptions other)
		{
			this.m_ApiVersion = 2;
			this.LeaderboardRecordIndex = other.LeaderboardRecordIndex;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000271B4 File Offset: 0x000253B4
		public void Set(ref CopyLeaderboardRecordByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LeaderboardRecordIndex = other.Value.LeaderboardRecordIndex;
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000271EA File Offset: 0x000253EA
		public void Dispose()
		{
		}

		// Token: 0x04000BBC RID: 3004
		private int m_ApiVersion;

		// Token: 0x04000BBD RID: 3005
		private uint m_LeaderboardRecordIndex;
	}
}
