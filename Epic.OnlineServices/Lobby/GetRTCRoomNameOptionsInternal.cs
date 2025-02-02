using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034B RID: 843
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetRTCRoomNameOptionsInternal : ISettable<GetRTCRoomNameOptions>, IDisposable
	{
		// Token: 0x1700063D RID: 1597
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x00020FC0 File Offset: 0x0001F1C0
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x00020FD0 File Offset: 0x0001F1D0
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00020FE0 File Offset: 0x0001F1E0
		public void Set(ref GetRTCRoomNameOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00021004 File Offset: 0x0001F204
		public void Set(ref GetRTCRoomNameOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0002104F File Offset: 0x0001F24F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A17 RID: 2583
		private int m_ApiVersion;

		// Token: 0x04000A18 RID: 2584
		private IntPtr m_LobbyId;

		// Token: 0x04000A19 RID: 2585
		private IntPtr m_LocalUserId;
	}
}
