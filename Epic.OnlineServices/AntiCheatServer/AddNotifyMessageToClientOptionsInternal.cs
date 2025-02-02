using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005BC RID: 1468
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToClientOptionsInternal : ISettable<AddNotifyMessageToClientOptions>, IDisposable
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x0003801C File Offset: 0x0003621C
		public void Set(ref AddNotifyMessageToClientOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00038028 File Offset: 0x00036228
		public void Set(ref AddNotifyMessageToClientOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00038049 File Offset: 0x00036249
		public void Dispose()
		{
		}

		// Token: 0x04001088 RID: 4232
		private int m_ApiVersion;
	}
}
