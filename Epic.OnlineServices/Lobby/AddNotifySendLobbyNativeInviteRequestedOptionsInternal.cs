using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032F RID: 815
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySendLobbyNativeInviteRequestedOptionsInternal : ISettable<AddNotifySendLobbyNativeInviteRequestedOptions>, IDisposable
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x0001FD86 File Offset: 0x0001DF86
		public void Set(ref AddNotifySendLobbyNativeInviteRequestedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0001FD90 File Offset: 0x0001DF90
		public void Set(ref AddNotifySendLobbyNativeInviteRequestedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0001FDB1 File Offset: 0x0001DFB1
		public void Dispose()
		{
		}

		// Token: 0x040009C0 RID: 2496
		private int m_ApiVersion;
	}
}
