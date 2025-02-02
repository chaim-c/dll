using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005B8 RID: 1464
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyClientActionRequiredOptionsInternal : ISettable<AddNotifyClientActionRequiredOptions>, IDisposable
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x00037FBE File Offset: 0x000361BE
		public void Set(ref AddNotifyClientActionRequiredOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00037FC8 File Offset: 0x000361C8
		public void Set(ref AddNotifyClientActionRequiredOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00037FE9 File Offset: 0x000361E9
		public void Dispose()
		{
		}

		// Token: 0x04001086 RID: 4230
		private int m_ApiVersion;
	}
}
