using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A4 RID: 676
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyIncomingPacketQueueFullOptionsInternal : ISettable<AddNotifyIncomingPacketQueueFullOptions>, IDisposable
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x0001B56F File Offset: 0x0001976F
		public void Set(ref AddNotifyIncomingPacketQueueFullOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0001B57C File Offset: 0x0001977C
		public void Set(ref AddNotifyIncomingPacketQueueFullOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0001B59D File Offset: 0x0001979D
		public void Dispose()
		{
		}

		// Token: 0x04000832 RID: 2098
		private int m_ApiVersion;
	}
}
