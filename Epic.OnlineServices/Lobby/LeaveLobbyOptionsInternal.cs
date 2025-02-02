using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000363 RID: 867
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyOptionsInternal : ISettable<LeaveLobbyOptions>, IDisposable
	{
		// Token: 0x1700068F RID: 1679
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x000221F4 File Offset: 0x000203F4
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000690 RID: 1680
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00022204 File Offset: 0x00020404
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00022214 File Offset: 0x00020414
		public void Set(ref LeaveLobbyOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00022238 File Offset: 0x00020438
		public void Set(ref LeaveLobbyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00022283 File Offset: 0x00020483
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x04000A69 RID: 2665
		private int m_ApiVersion;

		// Token: 0x04000A6A RID: 2666
		private IntPtr m_LocalUserId;

		// Token: 0x04000A6B RID: 2667
		private IntPtr m_LobbyId;
	}
}
