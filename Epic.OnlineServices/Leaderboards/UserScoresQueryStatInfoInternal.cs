using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000424 RID: 1060
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserScoresQueryStatInfoInternal : IGettable<UserScoresQueryStatInfo>, ISettable<UserScoresQueryStatInfo>, IDisposable
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00028710 File Offset: 0x00026910
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x00028731 File Offset: 0x00026931
		public Utf8String StatName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_StatName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_StatName);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00028744 File Offset: 0x00026944
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0002875C File Offset: 0x0002695C
		public LeaderboardAggregation Aggregation
		{
			get
			{
				return this.m_Aggregation;
			}
			set
			{
				this.m_Aggregation = value;
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00028766 File Offset: 0x00026966
		public void Set(ref UserScoresQueryStatInfo other)
		{
			this.m_ApiVersion = 1;
			this.StatName = other.StatName;
			this.Aggregation = other.Aggregation;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0002878C File Offset: 0x0002698C
		public void Set(ref UserScoresQueryStatInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.Value.StatName;
				this.Aggregation = other.Value.Aggregation;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000287D7 File Offset: 0x000269D7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000287E6 File Offset: 0x000269E6
		public void Get(out UserScoresQueryStatInfo output)
		{
			output = default(UserScoresQueryStatInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C26 RID: 3110
		private int m_ApiVersion;

		// Token: 0x04000C27 RID: 3111
		private IntPtr m_StatName;

		// Token: 0x04000C28 RID: 3112
		private LeaderboardAggregation m_Aggregation;
	}
}
