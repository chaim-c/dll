using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031F RID: 799
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinLobbyAcceptedOptionsInternal : ISettable<AddNotifyJoinLobbyAcceptedOptions>, IDisposable
	{
		// Token: 0x0600155F RID: 5471 RVA: 0x0001FB6A File Offset: 0x0001DD6A
		public void Set(ref AddNotifyJoinLobbyAcceptedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0001FB74 File Offset: 0x0001DD74
		public void Set(ref AddNotifyJoinLobbyAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0001FB95 File Offset: 0x0001DD95
		public void Dispose()
		{
		}

		// Token: 0x040009B4 RID: 2484
		private int m_ApiVersion;
	}
}
