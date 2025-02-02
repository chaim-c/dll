using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003F6 RID: 1014
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyOptionsInternal : ISettable<UpdateLobbyOptions>, IDisposable
	{
		// Token: 0x17000770 RID: 1904
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00027027 File Offset: 0x00025227
		public LobbyModification LobbyModificationHandle
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyModificationHandle);
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00027037 File Offset: 0x00025237
		public void Set(ref UpdateLobbyOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyModificationHandle = other.LobbyModificationHandle;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00027050 File Offset: 0x00025250
		public void Set(ref UpdateLobbyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyModificationHandle = other.Value.LobbyModificationHandle;
			}
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00027086 File Offset: 0x00025286
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LobbyModificationHandle);
		}

		// Token: 0x04000BB3 RID: 2995
		private int m_ApiVersion;

		// Token: 0x04000BB4 RID: 2996
		private IntPtr m_LobbyModificationHandle;
	}
}
