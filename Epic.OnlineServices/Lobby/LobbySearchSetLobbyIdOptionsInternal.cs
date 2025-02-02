using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A8 RID: 936
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetLobbyIdOptionsInternal : ISettable<LobbySearchSetLobbyIdOptions>, IDisposable
	{
		// Token: 0x17000708 RID: 1800
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x000256FB File Offset: 0x000238FB
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0002570B File Offset: 0x0002390B
		public void Set(ref LobbySearchSetLobbyIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00025724 File Offset: 0x00023924
		public void Set(ref LobbySearchSetLobbyIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0002575A File Offset: 0x0002395A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x04000B49 RID: 2889
		private int m_ApiVersion;

		// Token: 0x04000B4A RID: 2890
		private IntPtr m_LobbyId;
	}
}
