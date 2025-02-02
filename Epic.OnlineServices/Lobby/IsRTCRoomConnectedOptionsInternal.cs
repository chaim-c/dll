using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000351 RID: 849
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsRTCRoomConnectedOptionsInternal : ISettable<IsRTCRoomConnectedOptions>, IDisposable
	{
		// Token: 0x17000652 RID: 1618
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x0002143A File Offset: 0x0001F63A
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000653 RID: 1619
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0002144A File Offset: 0x0001F64A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0002145A File Offset: 0x0001F65A
		public void Set(ref IsRTCRoomConnectedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00021480 File Offset: 0x0001F680
		public void Set(ref IsRTCRoomConnectedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000214CB File Offset: 0x0001F6CB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A2D RID: 2605
		private int m_ApiVersion;

		// Token: 0x04000A2E RID: 2606
		private IntPtr m_LobbyId;

		// Token: 0x04000A2F RID: 2607
		private IntPtr m_LocalUserId;
	}
}
