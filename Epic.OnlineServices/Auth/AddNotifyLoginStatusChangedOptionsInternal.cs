using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000571 RID: 1393
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : ISettable<AddNotifyLoginStatusChangedOptions>, IDisposable
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x00034DAF File Offset: 0x00032FAF
		public void Set(ref AddNotifyLoginStatusChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00034DBC File Offset: 0x00032FBC
		public void Set(ref AddNotifyLoginStatusChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00034DDD File Offset: 0x00032FDD
		public void Dispose()
		{
		}

		// Token: 0x04000FAC RID: 4012
		private int m_ApiVersion;
	}
}
