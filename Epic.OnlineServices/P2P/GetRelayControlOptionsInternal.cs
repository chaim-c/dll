using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002BE RID: 702
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetRelayControlOptionsInternal : ISettable<GetRelayControlOptions>, IDisposable
	{
		// Token: 0x060012CF RID: 4815 RVA: 0x0001BD40 File Offset: 0x00019F40
		public void Set(ref GetRelayControlOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public void Set(ref GetRelayControlOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0001BD6D File Offset: 0x00019F6D
		public void Dispose()
		{
		}

		// Token: 0x04000871 RID: 2161
		private int m_ApiVersion;
	}
}
