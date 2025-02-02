using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200060D RID: 1549
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetGameSessionIdOptionsInternal : ISettable<SetGameSessionIdOptions>, IDisposable
	{
		// Token: 0x17000BF1 RID: 3057
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x0003B4F8 File Offset: 0x000396F8
		public Utf8String GameSessionId
		{
			set
			{
				Helper.Set(value, ref this.m_GameSessionId);
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0003B508 File Offset: 0x00039708
		public void Set(ref SetGameSessionIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.GameSessionId = other.GameSessionId;
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0003B520 File Offset: 0x00039720
		public void Set(ref SetGameSessionIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.GameSessionId = other.Value.GameSessionId;
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0003B556 File Offset: 0x00039756
		public void Dispose()
		{
			Helper.Dispose(ref this.m_GameSessionId);
		}

		// Token: 0x040011E4 RID: 4580
		private int m_ApiVersion;

		// Token: 0x040011E5 RID: 4581
		private IntPtr m_GameSessionId;
	}
}
