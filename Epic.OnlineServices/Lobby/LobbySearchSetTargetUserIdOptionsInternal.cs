using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AE RID: 942
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetTargetUserIdOptionsInternal : ISettable<LobbySearchSetTargetUserIdOptions>, IDisposable
	{
		// Token: 0x17000710 RID: 1808
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x000258A3 File Offset: 0x00023AA3
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000258B3 File Offset: 0x00023AB3
		public void Set(ref LobbySearchSetTargetUserIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000258CC File Offset: 0x00023ACC
		public void Set(ref LobbySearchSetTargetUserIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00025902 File Offset: 0x00023B02
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000B54 RID: 2900
		private int m_ApiVersion;

		// Token: 0x04000B55 RID: 2901
		private IntPtr m_TargetUserId;
	}
}
