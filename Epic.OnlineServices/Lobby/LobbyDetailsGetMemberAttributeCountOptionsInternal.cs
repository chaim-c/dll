using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000375 RID: 885
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberAttributeCountOptionsInternal : ISettable<LobbyDetailsGetMemberAttributeCountOptions>, IDisposable
	{
		// Token: 0x1700069E RID: 1694
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x000228ED File Offset: 0x00020AED
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000228FD File Offset: 0x00020AFD
		public void Set(ref LobbyDetailsGetMemberAttributeCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00022914 File Offset: 0x00020B14
		public void Set(ref LobbyDetailsGetMemberAttributeCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0002294A File Offset: 0x00020B4A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A8E RID: 2702
		private int m_ApiVersion;

		// Token: 0x04000A8F RID: 2703
		private IntPtr m_TargetUserId;
	}
}
