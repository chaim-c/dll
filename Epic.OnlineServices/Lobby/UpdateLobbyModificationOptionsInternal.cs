using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F4 RID: 1012
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyModificationOptionsInternal : ISettable<UpdateLobbyModificationOptions>, IDisposable
	{
		// Token: 0x1700076D RID: 1901
		// (set) Token: 0x06001A51 RID: 6737 RVA: 0x00026F6C File Offset: 0x0002516C
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x00026F7C File Offset: 0x0002517C
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00026F8C File Offset: 0x0002518C
		public void Set(ref UpdateLobbyModificationOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00026FB0 File Offset: 0x000251B0
		public void Set(ref UpdateLobbyModificationOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00026FFB File Offset: 0x000251FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x04000BAF RID: 2991
		private int m_ApiVersion;

		// Token: 0x04000BB0 RID: 2992
		private IntPtr m_LocalUserId;

		// Token: 0x04000BB1 RID: 2993
		private IntPtr m_LobbyId;
	}
}
