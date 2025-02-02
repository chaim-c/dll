using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003FA RID: 1018
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal : ISettable<CopyLeaderboardDefinitionByLeaderboardIdOptions>, IDisposable
	{
		// Token: 0x17000774 RID: 1908
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00027112 File Offset: 0x00025312
		public Utf8String LeaderboardId
		{
			set
			{
				Helper.Set(value, ref this.m_LeaderboardId);
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00027122 File Offset: 0x00025322
		public void Set(ref CopyLeaderboardDefinitionByLeaderboardIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LeaderboardId = other.LeaderboardId;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0002713C File Offset: 0x0002533C
		public void Set(ref CopyLeaderboardDefinitionByLeaderboardIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardId = other.Value.LeaderboardId;
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00027172 File Offset: 0x00025372
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LeaderboardId);
		}

		// Token: 0x04000BB9 RID: 3001
		private int m_ApiVersion;

		// Token: 0x04000BBA RID: 3002
		private IntPtr m_LeaderboardId;
	}
}
