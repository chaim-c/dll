using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000345 RID: 837
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyOptionsInternal : ISettable<DestroyLobbyOptions>, IDisposable
	{
		// Token: 0x17000633 RID: 1587
		// (set) Token: 0x06001622 RID: 5666 RVA: 0x00020DB8 File Offset: 0x0001EFB8
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000634 RID: 1588
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref this.m_LobbyId);
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		public void Set(ref DestroyLobbyOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.LobbyId = other.LobbyId;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00020DFC File Offset: 0x0001EFFC
		public void Set(ref DestroyLobbyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00020E47 File Offset: 0x0001F047
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_LobbyId);
		}

		// Token: 0x04000A0A RID: 2570
		private int m_ApiVersion;

		// Token: 0x04000A0B RID: 2571
		private IntPtr m_LocalUserId;

		// Token: 0x04000A0C RID: 2572
		private IntPtr m_LobbyId;
	}
}
