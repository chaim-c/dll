using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000337 RID: 823
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByInviteIdOptionsInternal : ISettable<CopyLobbyDetailsHandleByInviteIdOptions>, IDisposable
	{
		// Token: 0x17000602 RID: 1538
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x0002045B File Offset: 0x0001E65B
		public Utf8String InviteId
		{
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0002046B File Offset: 0x0001E66B
		public void Set(ref CopyLobbyDetailsHandleByInviteIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.InviteId = other.InviteId;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00020484 File Offset: 0x0001E684
		public void Set(ref CopyLobbyDetailsHandleByInviteIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x000204BA File Offset: 0x0001E6BA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x040009D6 RID: 2518
		private int m_ApiVersion;

		// Token: 0x040009D7 RID: 2519
		private IntPtr m_InviteId;
	}
}
