using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000394 RID: 916
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetInvitesAllowedOptionsInternal : ISettable<LobbyModificationSetInvitesAllowedOptions>, IDisposable
	{
		// Token: 0x170006F5 RID: 1781
		// (set) Token: 0x0600185B RID: 6235 RVA: 0x00025026 File Offset: 0x00023226
		public bool InvitesAllowed
		{
			set
			{
				Helper.Set(value, ref this.m_InvitesAllowed);
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00025036 File Offset: 0x00023236
		public void Set(ref LobbyModificationSetInvitesAllowedOptions other)
		{
			this.m_ApiVersion = 1;
			this.InvitesAllowed = other.InvitesAllowed;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00025050 File Offset: 0x00023250
		public void Set(ref LobbyModificationSetInvitesAllowedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.InvitesAllowed = other.Value.InvitesAllowed;
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00025086 File Offset: 0x00023286
		public void Dispose()
		{
		}

		// Token: 0x04000B24 RID: 2852
		private int m_ApiVersion;

		// Token: 0x04000B25 RID: 2853
		private int m_InvitesAllowed;
	}
}
