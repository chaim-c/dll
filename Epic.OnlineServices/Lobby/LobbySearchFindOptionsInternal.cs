using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A0 RID: 928
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindOptionsInternal : ISettable<LobbySearchFindOptions>, IDisposable
	{
		// Token: 0x17000702 RID: 1794
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00025592 File Offset: 0x00023792
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000255A2 File Offset: 0x000237A2
		public void Set(ref LobbySearchFindOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000255BC File Offset: 0x000237BC
		public void Set(ref LobbySearchFindOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000255F2 File Offset: 0x000237F2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B40 RID: 2880
		private int m_ApiVersion;

		// Token: 0x04000B41 RID: 2881
		private IntPtr m_LocalUserId;
	}
}
