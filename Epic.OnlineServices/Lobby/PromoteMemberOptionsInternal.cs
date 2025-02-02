using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E0 RID: 992
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberOptionsInternal : ISettable<PromoteMemberOptions>, IDisposable
	{
		// Token: 0x17000728 RID: 1832
		// (set) Token: 0x060019A6 RID: 6566 RVA: 0x00025E7D File Offset: 0x0002407D
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000729 RID: 1833
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x00025E8D File Offset: 0x0002408D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700072A RID: 1834
		// (set) Token: 0x060019A8 RID: 6568 RVA: 0x00025E9D File Offset: 0x0002409D
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00025EAD File Offset: 0x000240AD
		public void Set(ref PromoteMemberOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00025EE0 File Offset: 0x000240E0
		public void Set(ref PromoteMemberOptions? other)
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

		// Token: 0x060019AB RID: 6571 RVA: 0x00025F40 File Offset: 0x00024140
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000B6C RID: 2924
		private int m_ApiVersion;

		// Token: 0x04000B6D RID: 2925
		private IntPtr m_LobbyId;

		// Token: 0x04000B6E RID: 2926
		private IntPtr m_LocalUserId;

		// Token: 0x04000B6F RID: 2927
		private IntPtr m_TargetUserId;
	}
}
