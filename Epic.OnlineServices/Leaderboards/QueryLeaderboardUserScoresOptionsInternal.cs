using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000422 RID: 1058
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardUserScoresOptionsInternal : ISettable<QueryLeaderboardUserScoresOptions>, IDisposable
	{
		// Token: 0x170007B7 RID: 1975
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0002856B File Offset: 0x0002676B
		public ProductUserId[] UserIds
		{
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_UserIds, out this.m_UserIdsCount);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x00028581 File Offset: 0x00026781
		public UserScoresQueryStatInfo[] StatInfo
		{
			set
			{
				Helper.Set<UserScoresQueryStatInfo, UserScoresQueryStatInfoInternal>(ref value, ref this.m_StatInfo, out this.m_StatInfoCount);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x00028598 File Offset: 0x00026798
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.Set(value, ref this.m_StartTime);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x000285A8 File Offset: 0x000267A8
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.Set(value, ref this.m_EndTime);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x000285B8 File Offset: 0x000267B8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000285C8 File Offset: 0x000267C8
		public void Set(ref QueryLeaderboardUserScoresOptions other)
		{
			this.m_ApiVersion = 2;
			this.UserIds = other.UserIds;
			this.StatInfo = other.StatInfo;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00028620 File Offset: 0x00026820
		public void Set(ref QueryLeaderboardUserScoresOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UserIds = other.Value.UserIds;
				this.StatInfo = other.Value.StatInfo;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000286AA File Offset: 0x000268AA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserIds);
			Helper.Dispose(ref this.m_StatInfo);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C1C RID: 3100
		private int m_ApiVersion;

		// Token: 0x04000C1D RID: 3101
		private IntPtr m_UserIds;

		// Token: 0x04000C1E RID: 3102
		private uint m_UserIdsCount;

		// Token: 0x04000C1F RID: 3103
		private IntPtr m_StatInfo;

		// Token: 0x04000C20 RID: 3104
		private uint m_StatInfoCount;

		// Token: 0x04000C21 RID: 3105
		private long m_StartTime;

		// Token: 0x04000C22 RID: 3106
		private long m_EndTime;

		// Token: 0x04000C23 RID: 3107
		private IntPtr m_LocalUserId;
	}
}
