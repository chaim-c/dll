using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003EE RID: 1006
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable<SendInviteOptions>, IDisposable
	{
		// Token: 0x17000754 RID: 1876
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x00026929 File Offset: 0x00024B29
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x00026939 File Offset: 0x00024B39
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00026949 File Offset: 0x00024B49
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00026959 File Offset: 0x00024B59
		public void Set(ref SendInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0002698C File Offset: 0x00024B8C
		public void Set(ref SendInviteOptions? other)
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

		// Token: 0x06001A1A RID: 6682 RVA: 0x000269EC File Offset: 0x00024BEC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000B97 RID: 2967
		private int m_ApiVersion;

		// Token: 0x04000B98 RID: 2968
		private IntPtr m_LobbyId;

		// Token: 0x04000B99 RID: 2969
		private IntPtr m_LocalUserId;

		// Token: 0x04000B9A RID: 2970
		private IntPtr m_TargetUserId;
	}
}
