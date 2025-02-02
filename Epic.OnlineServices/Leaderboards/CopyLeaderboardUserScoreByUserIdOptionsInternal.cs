using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000402 RID: 1026
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByUserIdOptionsInternal : ISettable<CopyLeaderboardUserScoreByUserIdOptions>, IDisposable
	{
		// Token: 0x1700077F RID: 1919
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x0002734C File Offset: 0x0002554C
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000780 RID: 1920
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0002735C File Offset: 0x0002555C
		public Utf8String StatName
		{
			set
			{
				Helper.Set(value, ref this.m_StatName);
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0002736C File Offset: 0x0002556C
		public void Set(ref CopyLeaderboardUserScoreByUserIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
			this.StatName = other.StatName;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00027390 File Offset: 0x00025590
		public void Set(ref CopyLeaderboardUserScoreByUserIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
				this.StatName = other.Value.StatName;
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000273DB File Offset: 0x000255DB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_StatName);
		}

		// Token: 0x04000BC8 RID: 3016
		private int m_ApiVersion;

		// Token: 0x04000BC9 RID: 3017
		private IntPtr m_UserId;

		// Token: 0x04000BCA RID: 3018
		private IntPtr m_StatName;
	}
}
