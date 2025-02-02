using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B8 RID: 184
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryStatsOptionsInternal : ISettable<QueryStatsOptions>, IDisposable
	{
		// Token: 0x1700013D RID: 317
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00009DF8 File Offset: 0x00007FF8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700013E RID: 318
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00009E08 File Offset: 0x00008008
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.Set(value, ref this.m_StartTime);
			}
		}

		// Token: 0x1700013F RID: 319
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00009E18 File Offset: 0x00008018
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.Set(value, ref this.m_EndTime);
			}
		}

		// Token: 0x17000140 RID: 320
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00009E28 File Offset: 0x00008028
		public Utf8String[] StatNames
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_StatNames, true, out this.m_StatNamesCount);
			}
		}

		// Token: 0x17000141 RID: 321
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00009E3F File Offset: 0x0000803F
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00009E50 File Offset: 0x00008050
		public void Set(ref QueryStatsOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.StartTime = other.StartTime;
			this.EndTime = other.EndTime;
			this.StatNames = other.StatNames;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00009EA8 File Offset: 0x000080A8
		public void Set(ref QueryStatsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
				this.StatNames = other.Value.StatNames;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00009F32 File Offset: 0x00008132
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_StatNames);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000328 RID: 808
		private int m_ApiVersion;

		// Token: 0x04000329 RID: 809
		private IntPtr m_LocalUserId;

		// Token: 0x0400032A RID: 810
		private long m_StartTime;

		// Token: 0x0400032B RID: 811
		private long m_EndTime;

		// Token: 0x0400032C RID: 812
		private IntPtr m_StatNames;

		// Token: 0x0400032D RID: 813
		private uint m_StatNamesCount;

		// Token: 0x0400032E RID: 814
		private IntPtr m_TargetUserId;
	}
}
