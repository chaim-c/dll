using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000379 RID: 889
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberCountOptionsInternal : ISettable<LobbyDetailsGetMemberCountOptions>, IDisposable
	{
		// Token: 0x06001740 RID: 5952 RVA: 0x000229C5 File Offset: 0x00020BC5
		public void Set(ref LobbyDetailsGetMemberCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000229D0 File Offset: 0x00020BD0
		public void Set(ref LobbyDetailsGetMemberCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000229F1 File Offset: 0x00020BF1
		public void Dispose()
		{
		}

		// Token: 0x04000A93 RID: 2707
		private int m_ApiVersion;
	}
}
