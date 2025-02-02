using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005EB RID: 1515
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogGameRoundEndOptionsInternal : ISettable<LogGameRoundEndOptions>, IDisposable
	{
		// Token: 0x17000B62 RID: 2914
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x00039997 File Offset: 0x00037B97
		public uint WinningTeamId
		{
			set
			{
				this.m_WinningTeamId = value;
			}
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000399A1 File Offset: 0x00037BA1
		public void Set(ref LogGameRoundEndOptions other)
		{
			this.m_ApiVersion = 1;
			this.WinningTeamId = other.WinningTeamId;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000399B8 File Offset: 0x00037BB8
		public void Set(ref LogGameRoundEndOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.WinningTeamId = other.Value.WinningTeamId;
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000399EE File Offset: 0x00037BEE
		public void Dispose()
		{
		}

		// Token: 0x0400114B RID: 4427
		private int m_ApiVersion;

		// Token: 0x0400114C RID: 4428
		private uint m_WinningTeamId;
	}
}
