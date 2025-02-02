using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002DE RID: 734
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryNATTypeOptionsInternal : ISettable<QueryNATTypeOptions>, IDisposable
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x0001D7A0 File Offset: 0x0001B9A0
		public void Set(ref QueryNATTypeOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0001D7AC File Offset: 0x0001B9AC
		public void Set(ref QueryNATTypeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0001D7CD File Offset: 0x0001B9CD
		public void Dispose()
		{
		}

		// Token: 0x040008DC RID: 2268
		private int m_ApiVersion;
	}
}
