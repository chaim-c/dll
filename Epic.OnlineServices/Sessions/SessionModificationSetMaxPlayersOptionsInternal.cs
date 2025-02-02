using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013F RID: 319
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetMaxPlayersOptionsInternal : ISettable<SessionModificationSetMaxPlayersOptions>, IDisposable
	{
		// Token: 0x17000210 RID: 528
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0000DA82 File Offset: 0x0000BC82
		public uint MaxPlayers
		{
			set
			{
				this.m_MaxPlayers = value;
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0000DA8C File Offset: 0x0000BC8C
		public void Set(ref SessionModificationSetMaxPlayersOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxPlayers = other.MaxPlayers;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		public void Set(ref SessionModificationSetMaxPlayersOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxPlayers = other.Value.MaxPlayers;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000DADA File Offset: 0x0000BCDA
		public void Dispose()
		{
		}

		// Token: 0x04000450 RID: 1104
		private int m_ApiVersion;

		// Token: 0x04000451 RID: 1105
		private uint m_MaxPlayers;
	}
}
