using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000510 RID: 1296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLoginStatusChangedOptionsInternal : ISettable<AddNotifyLoginStatusChangedOptions>, IDisposable
	{
		// Token: 0x06002144 RID: 8516 RVA: 0x00031670 File Offset: 0x0002F870
		public void Set(ref AddNotifyLoginStatusChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0003167C File Offset: 0x0002F87C
		public void Set(ref AddNotifyLoginStatusChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0003169D File Offset: 0x0002F89D
		public void Dispose()
		{
		}

		// Token: 0x04000EC5 RID: 3781
		private int m_ApiVersion;
	}
}
