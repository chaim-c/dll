using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032B RID: 811
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyUpdateReceivedOptionsInternal : ISettable<AddNotifyLobbyUpdateReceivedOptions>, IDisposable
	{
		// Token: 0x06001571 RID: 5489 RVA: 0x0001FC88 File Offset: 0x0001DE88
		public void Set(ref AddNotifyLobbyUpdateReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0001FC94 File Offset: 0x0001DE94
		public void Set(ref AddNotifyLobbyUpdateReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
		public void Dispose()
		{
		}

		// Token: 0x040009BA RID: 2490
		private int m_ApiVersion;
	}
}
