using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035F RID: 863
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberOptionsInternal : ISettable<KickMemberOptions>, IDisposable
	{
		// Token: 0x17000683 RID: 1667
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x00021F19 File Offset: 0x00020119
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000684 RID: 1668
		// (set) Token: 0x060016DF RID: 5855 RVA: 0x00021F29 File Offset: 0x00020129
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00021F39 File Offset: 0x00020139
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00021F49 File Offset: 0x00020149
		public void Set(ref KickMemberOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00021F7C File Offset: 0x0002017C
		public void Set(ref KickMemberOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00021FDC File Offset: 0x000201DC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A5D RID: 2653
		private int m_ApiVersion;

		// Token: 0x04000A5E RID: 2654
		private IntPtr m_LobbyId;

		// Token: 0x04000A5F RID: 2655
		private IntPtr m_LocalUserId;

		// Token: 0x04000A60 RID: 2656
		private IntPtr m_TargetUserId;
	}
}
