using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002BA RID: 698
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPacketQueueInfoOptionsInternal : ISettable<GetPacketQueueInfoOptions>, IDisposable
	{
		// Token: 0x060012C9 RID: 4809 RVA: 0x0001BCE2 File Offset: 0x00019EE2
		public void Set(ref GetPacketQueueInfoOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public void Set(ref GetPacketQueueInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0001BD0D File Offset: 0x00019F0D
		public void Dispose()
		{
		}

		// Token: 0x0400086F RID: 2159
		private int m_ApiVersion;
	}
}
