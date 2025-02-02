using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034F RID: 847
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HardMuteMemberOptionsInternal : ISettable<HardMuteMemberOptions>, IDisposable
	{
		// Token: 0x1700064C RID: 1612
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x000212FB File Offset: 0x0001F4FB
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x1700064D RID: 1613
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0002130B File Offset: 0x0001F50B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700064E RID: 1614
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0002131B File Offset: 0x0001F51B
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700064F RID: 1615
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0002132B File Offset: 0x0001F52B
		public bool HardMute
		{
			set
			{
				Helper.Set(value, ref this.m_HardMute);
			}
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0002133B File Offset: 0x0001F53B
		public void Set(ref HardMuteMemberOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.HardMute = other.HardMute;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0002137C File Offset: 0x0001F57C
		public void Set(ref HardMuteMemberOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.Value.LobbyId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.HardMute = other.Value.HardMute;
			}
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000213F1 File Offset: 0x0001F5F1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A26 RID: 2598
		private int m_ApiVersion;

		// Token: 0x04000A27 RID: 2599
		private IntPtr m_LobbyId;

		// Token: 0x04000A28 RID: 2600
		private IntPtr m_LocalUserId;

		// Token: 0x04000A29 RID: 2601
		private IntPtr m_TargetUserId;

		// Token: 0x04000A2A RID: 2602
		private int m_HardMute;
	}
}
