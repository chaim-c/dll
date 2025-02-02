using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004EF RID: 1263
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionGetEntitlementsCountOptionsInternal : ISettable<TransactionGetEntitlementsCountOptions>, IDisposable
	{
		// Token: 0x0600207D RID: 8317 RVA: 0x00030415 File Offset: 0x0002E615
		public void Set(ref TransactionGetEntitlementsCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00030420 File Offset: 0x0002E620
		public void Set(ref TransactionGetEntitlementsCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00030441 File Offset: 0x0002E641
		public void Dispose()
		{
		}

		// Token: 0x04000E7E RID: 3710
		private int m_ApiVersion;
	}
}
