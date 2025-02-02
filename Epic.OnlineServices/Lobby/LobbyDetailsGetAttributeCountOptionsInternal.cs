using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000371 RID: 881
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetAttributeCountOptionsInternal : ISettable<LobbyDetailsGetAttributeCountOptions>, IDisposable
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x0002287E File Offset: 0x00020A7E
		public void Set(ref LobbyDetailsGetAttributeCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00022888 File Offset: 0x00020A88
		public void Set(ref LobbyDetailsGetAttributeCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000228A9 File Offset: 0x00020AA9
		public void Dispose()
		{
		}

		// Token: 0x04000A8B RID: 2699
		private int m_ApiVersion;
	}
}
