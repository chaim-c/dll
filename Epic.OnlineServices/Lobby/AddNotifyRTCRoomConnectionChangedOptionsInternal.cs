using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032D RID: 813
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyRTCRoomConnectionChangedOptionsInternal : ISettable<AddNotifyRTCRoomConnectionChangedOptions>, IDisposable
	{
		// Token: 0x170005EE RID: 1518
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x0001FCDA File Offset: 0x0001DEDA
		public Utf8String LobbyId_DEPRECATED
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId_DEPRECATED);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x0001FCEA File Offset: 0x0001DEEA
		public ProductUserId LocalUserId_DEPRECATED
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId_DEPRECATED);
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0001FCFA File Offset: 0x0001DEFA
		public void Set(ref AddNotifyRTCRoomConnectionChangedOptions other)
		{
			this.m_ApiVersion = 2;
			this.LobbyId_DEPRECATED = other.LobbyId_DEPRECATED;
			this.LocalUserId_DEPRECATED = other.LocalUserId_DEPRECATED;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public void Set(ref AddNotifyRTCRoomConnectionChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LobbyId_DEPRECATED = other.Value.LobbyId_DEPRECATED;
				this.LocalUserId_DEPRECATED = other.Value.LocalUserId_DEPRECATED;
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0001FD6B File Offset: 0x0001DF6B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId_DEPRECATED);
			Helper.Dispose(ref this.m_LocalUserId_DEPRECATED);
		}

		// Token: 0x040009BD RID: 2493
		private int m_ApiVersion;

		// Token: 0x040009BE RID: 2494
		private IntPtr m_LobbyId_DEPRECATED;

		// Token: 0x040009BF RID: 2495
		private IntPtr m_LocalUserId_DEPRECATED;
	}
}
