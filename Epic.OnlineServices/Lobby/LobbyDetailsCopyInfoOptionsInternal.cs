using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036B RID: 875
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyInfoOptionsInternal : ISettable<LobbyDetailsCopyInfoOptions>, IDisposable
	{
		// Token: 0x06001719 RID: 5913 RVA: 0x000226C9 File Offset: 0x000208C9
		public void Set(ref LobbyDetailsCopyInfoOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000226D4 File Offset: 0x000208D4
		public void Set(ref LobbyDetailsCopyInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000226F5 File Offset: 0x000208F5
		public void Dispose()
		{
		}

		// Token: 0x04000A80 RID: 2688
		private int m_ApiVersion;
	}
}
