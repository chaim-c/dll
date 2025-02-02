using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000446 RID: 1094
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryAgeGateOptionsInternal : ISettable<QueryAgeGateOptions>, IDisposable
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x00029727 File Offset: 0x00027927
		public void Set(ref QueryAgeGateOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x00029734 File Offset: 0x00027934
		public void Set(ref QueryAgeGateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00029755 File Offset: 0x00027955
		public void Dispose()
		{
		}

		// Token: 0x04000C69 RID: 3177
		private int m_ApiVersion;
	}
}
