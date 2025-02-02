using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200040A RID: 1034
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardUserScoreCountOptionsInternal : ISettable<GetLeaderboardUserScoreCountOptions>, IDisposable
	{
		// Token: 0x1700078C RID: 1932
		// (set) Token: 0x06001AA7 RID: 6823 RVA: 0x00027711 File Offset: 0x00025911
		public Utf8String StatName
		{
			set
			{
				Helper.Set(value, ref this.m_StatName);
			}
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00027721 File Offset: 0x00025921
		public void Set(ref GetLeaderboardUserScoreCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.StatName = other.StatName;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00027738 File Offset: 0x00025938
		public void Set(ref GetLeaderboardUserScoreCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.Value.StatName;
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0002776E File Offset: 0x0002596E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x04000BD9 RID: 3033
		private int m_ApiVersion;

		// Token: 0x04000BDA RID: 3034
		private IntPtr m_StatName;
	}
}
