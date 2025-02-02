using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000420 RID: 1056
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardRanksOptionsInternal : ISettable<QueryLeaderboardRanksOptions>, IDisposable
	{
		// Token: 0x170007B0 RID: 1968
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x00028469 File Offset: 0x00026669
		public Utf8String LeaderboardId
		{
			set
			{
				Helper.Set(value, ref this.m_LeaderboardId);
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00028479 File Offset: 0x00026679
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00028489 File Offset: 0x00026689
		public void Set(ref QueryLeaderboardRanksOptions other)
		{
			this.m_ApiVersion = 2;
			this.LeaderboardId = other.LeaderboardId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000284B0 File Offset: 0x000266B0
		public void Set(ref QueryLeaderboardRanksOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LeaderboardId = other.Value.LeaderboardId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000284FB File Offset: 0x000266FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LeaderboardId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C14 RID: 3092
		private int m_ApiVersion;

		// Token: 0x04000C15 RID: 3093
		private IntPtr m_LeaderboardId;

		// Token: 0x04000C16 RID: 3094
		private IntPtr m_LocalUserId;
	}
}
