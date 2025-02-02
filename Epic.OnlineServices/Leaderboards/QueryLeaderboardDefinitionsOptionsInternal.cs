using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041E RID: 1054
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardDefinitionsOptionsInternal : ISettable<QueryLeaderboardDefinitionsOptions>, IDisposable
	{
		// Token: 0x170007AB RID: 1963
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x00028374 File Offset: 0x00026574
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.Set(value, ref this.m_StartTime);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x00028384 File Offset: 0x00026584
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.Set(value, ref this.m_EndTime);
			}
		}

		// Token: 0x170007AD RID: 1965
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x00028394 File Offset: 0x00026594
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000283A4 File Offset: 0x000265A4
		public void Set(ref QueryLeaderboardDefinitionsOptions other)
		{
			this.m_ApiVersion = 2;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000283D8 File Offset: 0x000265D8
		public void Set(ref QueryLeaderboardDefinitionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00028438 File Offset: 0x00026638
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C0E RID: 3086
		private int m_ApiVersion;

		// Token: 0x04000C0F RID: 3087
		private long m_StartTime;

		// Token: 0x04000C10 RID: 3088
		private long m_EndTime;

		// Token: 0x04000C11 RID: 3089
		private IntPtr m_LocalUserId;
	}
}
