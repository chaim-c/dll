using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000357 RID: 855
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyByIdOptionsInternal : ISettable<JoinLobbyByIdOptions>, IDisposable
	{
		// Token: 0x17000666 RID: 1638
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x000218CA File Offset: 0x0001FACA
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000667 RID: 1639
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x000218DA File Offset: 0x0001FADA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000668 RID: 1640
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x000218EA File Offset: 0x0001FAEA
		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceEnabled);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x000218FA File Offset: 0x0001FAFA
		public LocalRTCOptions? LocalRTCOptions
		{
			set
			{
				Helper.Set<LocalRTCOptions, LocalRTCOptionsInternal>(ref value, ref this.m_LocalRTCOptions);
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0002190B File Offset: 0x0001FB0B
		public void Set(ref JoinLobbyByIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.PresenceEnabled = other.PresenceEnabled;
			this.LocalRTCOptions = other.LocalRTCOptions;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0002194C File Offset: 0x0001FB4C
		public void Set(ref JoinLobbyByIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceEnabled = other.Value.PresenceEnabled;
				this.LocalRTCOptions = other.Value.LocalRTCOptions;
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000219C1 File Offset: 0x0001FBC1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_LocalRTCOptions);
		}

		// Token: 0x04000A40 RID: 2624
		private int m_ApiVersion;

		// Token: 0x04000A41 RID: 2625
		private IntPtr m_LobbyId;

		// Token: 0x04000A42 RID: 2626
		private IntPtr m_LocalUserId;

		// Token: 0x04000A43 RID: 2627
		private int m_PresenceEnabled;

		// Token: 0x04000A44 RID: 2628
		private IntPtr m_LocalRTCOptions;
	}
}
