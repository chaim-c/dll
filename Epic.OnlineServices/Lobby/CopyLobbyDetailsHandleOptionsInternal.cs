using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033B RID: 827
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleOptionsInternal : ISettable<CopyLobbyDetailsHandleOptions>, IDisposable
	{
		// Token: 0x17000607 RID: 1543
		// (set) Token: 0x060015C6 RID: 5574 RVA: 0x00020557 File Offset: 0x0001E757
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000608 RID: 1544
		// (set) Token: 0x060015C7 RID: 5575 RVA: 0x00020567 File Offset: 0x0001E767
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00020577 File Offset: 0x0001E777
		public void Set(ref CopyLobbyDetailsHandleOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0002059C File Offset: 0x0001E79C
		public void Set(ref CopyLobbyDetailsHandleOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000205E7 File Offset: 0x0001E7E7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040009DD RID: 2525
		private int m_ApiVersion;

		// Token: 0x040009DE RID: 2526
		private IntPtr m_LobbyId;

		// Token: 0x040009DF RID: 2527
		private IntPtr m_LocalUserId;
	}
}
