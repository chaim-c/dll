using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003FE RID: 1022
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByUserIdOptionsInternal : ISettable<CopyLeaderboardRecordByUserIdOptions>, IDisposable
	{
		// Token: 0x17000778 RID: 1912
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x000271FE File Offset: 0x000253FE
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0002720E File Offset: 0x0002540E
		public void Set(ref CopyLeaderboardRecordByUserIdOptions other)
		{
			this.m_ApiVersion = 2;
			this.UserId = other.UserId;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00027228 File Offset: 0x00025428
		public void Set(ref CopyLeaderboardRecordByUserIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0002725E File Offset: 0x0002545E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x04000BBF RID: 3007
		private int m_ApiVersion;

		// Token: 0x04000BC0 RID: 3008
		private IntPtr m_UserId;
	}
}
