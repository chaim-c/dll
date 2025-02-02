using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002BC RID: 700
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPortRangeOptionsInternal : ISettable<GetPortRangeOptions>, IDisposable
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0001BD10 File Offset: 0x00019F10
		public void Set(ref GetPortRangeOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0001BD1C File Offset: 0x00019F1C
		public void Set(ref GetPortRangeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0001BD3D File Offset: 0x00019F3D
		public void Dispose()
		{
		}

		// Token: 0x04000870 RID: 2160
		private int m_ApiVersion;
	}
}
