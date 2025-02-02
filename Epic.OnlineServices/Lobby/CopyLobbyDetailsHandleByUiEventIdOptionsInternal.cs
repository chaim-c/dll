using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000339 RID: 825
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByUiEventIdOptionsInternal : ISettable<CopyLobbyDetailsHandleByUiEventIdOptions>, IDisposable
	{
		// Token: 0x17000604 RID: 1540
		// (set) Token: 0x060015BE RID: 5566 RVA: 0x000204DA File Offset: 0x0001E6DA
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000204E4 File Offset: 0x0001E6E4
		public void Set(ref CopyLobbyDetailsHandleByUiEventIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.UiEventId = other.UiEventId;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x000204FC File Offset: 0x0001E6FC
		public void Set(ref CopyLobbyDetailsHandleByUiEventIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00020532 File Offset: 0x0001E732
		public void Dispose()
		{
		}

		// Token: 0x040009D9 RID: 2521
		private int m_ApiVersion;

		// Token: 0x040009DA RID: 2522
		private ulong m_UiEventId;
	}
}
