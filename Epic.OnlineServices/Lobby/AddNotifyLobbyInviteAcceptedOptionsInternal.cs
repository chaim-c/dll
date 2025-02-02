using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000321 RID: 801
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteAcceptedOptionsInternal : ISettable<AddNotifyLobbyInviteAcceptedOptions>, IDisposable
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public void Set(ref AddNotifyLobbyInviteAcceptedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		public void Set(ref AddNotifyLobbyInviteAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0001FBC5 File Offset: 0x0001DDC5
		public void Dispose()
		{
		}

		// Token: 0x040009B5 RID: 2485
		private int m_ApiVersion;
	}
}
