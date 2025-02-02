using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000327 RID: 807
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberStatusReceivedOptionsInternal : ISettable<AddNotifyLobbyMemberStatusReceivedOptions>, IDisposable
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x0001FC28 File Offset: 0x0001DE28
		public void Set(ref AddNotifyLobbyMemberStatusReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0001FC34 File Offset: 0x0001DE34
		public void Set(ref AddNotifyLobbyMemberStatusReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0001FC55 File Offset: 0x0001DE55
		public void Dispose()
		{
		}

		// Token: 0x040009B8 RID: 2488
		private int m_ApiVersion;
	}
}
