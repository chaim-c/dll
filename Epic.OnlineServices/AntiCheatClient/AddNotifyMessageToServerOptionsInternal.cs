using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000617 RID: 1559
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToServerOptionsInternal : ISettable<AddNotifyMessageToServerOptions>, IDisposable
	{
		// Token: 0x060027EC RID: 10220 RVA: 0x0003B7A4 File Offset: 0x000399A4
		public void Set(ref AddNotifyMessageToServerOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0003B7B0 File Offset: 0x000399B0
		public void Set(ref AddNotifyMessageToServerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0003B7D1 File Offset: 0x000399D1
		public void Dispose()
		{
		}

		// Token: 0x040011F1 RID: 4593
		private int m_ApiVersion;
	}
}
