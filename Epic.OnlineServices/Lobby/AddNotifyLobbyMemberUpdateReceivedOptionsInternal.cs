using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000329 RID: 809
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberUpdateReceivedOptionsInternal : ISettable<AddNotifyLobbyMemberUpdateReceivedOptions>, IDisposable
	{
		// Token: 0x0600156E RID: 5486 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public void Set(ref AddNotifyLobbyMemberUpdateReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0001FC64 File Offset: 0x0001DE64
		public void Set(ref AddNotifyLobbyMemberUpdateReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0001FC85 File Offset: 0x0001DE85
		public void Dispose()
		{
		}

		// Token: 0x040009B9 RID: 2489
		private int m_ApiVersion;
	}
}
