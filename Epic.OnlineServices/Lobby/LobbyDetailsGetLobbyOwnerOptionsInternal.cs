using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000373 RID: 883
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetLobbyOwnerOptionsInternal : ISettable<LobbyDetailsGetLobbyOwnerOptions>, IDisposable
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x000228AC File Offset: 0x00020AAC
		public void Set(ref LobbyDetailsGetLobbyOwnerOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000228B8 File Offset: 0x00020AB8
		public void Set(ref LobbyDetailsGetLobbyOwnerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000228D9 File Offset: 0x00020AD9
		public void Dispose()
		{
		}

		// Token: 0x04000A8C RID: 2700
		private int m_ApiVersion;
	}
}
