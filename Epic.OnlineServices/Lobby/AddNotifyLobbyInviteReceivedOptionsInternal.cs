using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000323 RID: 803
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteReceivedOptionsInternal : ISettable<AddNotifyLobbyInviteReceivedOptions>, IDisposable
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		public void Set(ref AddNotifyLobbyInviteReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0001FBD4 File Offset: 0x0001DDD4
		public void Set(ref AddNotifyLobbyInviteReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0001FBF5 File Offset: 0x0001DDF5
		public void Dispose()
		{
		}

		// Token: 0x040009B6 RID: 2486
		private int m_ApiVersion;
	}
}
