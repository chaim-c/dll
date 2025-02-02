using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B6 RID: 694
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNATTypeOptionsInternal : ISettable<GetNATTypeOptions>, IDisposable
	{
		// Token: 0x060012BD RID: 4797 RVA: 0x0001BBE6 File Offset: 0x00019DE6
		public void Set(ref GetNATTypeOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0001BBF0 File Offset: 0x00019DF0
		public void Set(ref GetNATTypeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0001BC11 File Offset: 0x00019E11
		public void Dispose()
		{
		}

		// Token: 0x04000869 RID: 2153
		private int m_ApiVersion;
	}
}
