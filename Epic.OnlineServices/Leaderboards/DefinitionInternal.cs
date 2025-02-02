using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000404 RID: 1028
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : IGettable<Definition>, ISettable<Definition>, IDisposable
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0002749C File Offset: 0x0002569C
		// (set) Token: 0x06001A92 RID: 6802 RVA: 0x000274BD File Offset: 0x000256BD
		public Utf8String LeaderboardId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LeaderboardId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LeaderboardId);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001A93 RID: 6803 RVA: 0x000274D0 File Offset: 0x000256D0
		// (set) Token: 0x06001A94 RID: 6804 RVA: 0x000274F1 File Offset: 0x000256F1
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00027504 File Offset: 0x00025704
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x0002751C File Offset: 0x0002571C
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

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00027528 File Offset: 0x00025728
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00027549 File Offset: 0x00025749
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_StartTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_StartTime);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0002755C File Offset: 0x0002575C
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0002757D File Offset: 0x0002577D
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_EndTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_EndTime);
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00027590 File Offset: 0x00025790
		public void Set(ref Definition other)
		{
			this.m_ApiVersion = 1;
			this.LeaderboardId = other.LeaderboardId;
			this.StatName = other.StatName;
			this.Aggregation = other.Aggregation;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000275E8 File Offset: 0x000257E8
		public void Set(ref Definition? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardId = other.Value.LeaderboardId;
				this.StatName = other.Value.StatName;
				this.Aggregation = other.Value.Aggregation;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x00027672 File Offset: 0x00025872
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LeaderboardId);
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0002768D File Offset: 0x0002588D
		public void Get(out Definition output)
		{
			output = default(Definition);
			output.Set(ref this);
		}

		// Token: 0x04000BD0 RID: 3024
		private int m_ApiVersion;

		// Token: 0x04000BD1 RID: 3025
		private IntPtr m_LeaderboardId;

		// Token: 0x04000BD2 RID: 3026
		private IntPtr m_StatName;

		// Token: 0x04000BD3 RID: 3027
		private LeaderboardAggregation m_Aggregation;

		// Token: 0x04000BD4 RID: 3028
		private long m_StartTime;

		// Token: 0x04000BD5 RID: 3029
		private long m_EndTime;
	}
}
