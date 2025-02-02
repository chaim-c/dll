using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000325 RID: 805
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteRejectedOptionsInternal : ISettable<AddNotifyLobbyInviteRejectedOptions>, IDisposable
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x0001FBF8 File Offset: 0x0001DDF8
		public void Set(ref AddNotifyLobbyInviteRejectedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0001FC04 File Offset: 0x0001DE04
		public void Set(ref AddNotifyLobbyInviteRejectedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0001FC25 File Offset: 0x0001DE25
		public void Dispose()
		{
		}

		// Token: 0x040009B7 RID: 2487
		private int m_ApiVersion;
	}
}
