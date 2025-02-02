using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035B RID: 859
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyOptionsInternal : ISettable<JoinLobbyOptions>, IDisposable
	{
		// Token: 0x17000675 RID: 1653
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x00021BFA File Offset: 0x0001FDFA
		public LobbyDetails LobbyDetailsHandle
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyDetailsHandle);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (set) Token: 0x060016BF RID: 5823 RVA: 0x00021C0A File Offset: 0x0001FE0A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000677 RID: 1655
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x00021C1A File Offset: 0x0001FE1A
		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceEnabled);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00021C2A File Offset: 0x0001FE2A
		public LocalRTCOptions? LocalRTCOptions
		{
			set
			{
				Helper.Set<LocalRTCOptions, LocalRTCOptionsInternal>(ref value, ref this.m_LocalRTCOptions);
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00021C3B File Offset: 0x0001FE3B
		public void Set(ref JoinLobbyOptions other)
		{
			this.m_ApiVersion = 3;
			this.LobbyDetailsHandle = other.LobbyDetailsHandle;
			this.LocalUserId = other.LocalUserId;
			this.PresenceEnabled = other.PresenceEnabled;
			this.LocalRTCOptions = other.LocalRTCOptions;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00021C7C File Offset: 0x0001FE7C
		public void Set(ref JoinLobbyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LobbyDetailsHandle = other.Value.LobbyDetailsHandle;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceEnabled = other.Value.PresenceEnabled;
				this.LocalRTCOptions = other.Value.LocalRTCOptions;
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00021CF1 File Offset: 0x0001FEF1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyDetailsHandle);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_LocalRTCOptions);
		}

		// Token: 0x04000A4F RID: 2639
		private int m_ApiVersion;

		// Token: 0x04000A50 RID: 2640
		private IntPtr m_LobbyDetailsHandle;

		// Token: 0x04000A51 RID: 2641
		private IntPtr m_LocalUserId;

		// Token: 0x04000A52 RID: 2642
		private int m_PresenceEnabled;

		// Token: 0x04000A53 RID: 2643
		private IntPtr m_LocalRTCOptions;
	}
}
