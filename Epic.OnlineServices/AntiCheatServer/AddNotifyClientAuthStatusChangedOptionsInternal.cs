using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005BA RID: 1466
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyClientAuthStatusChangedOptionsInternal : ISettable<AddNotifyClientAuthStatusChangedOptions>, IDisposable
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x00037FEC File Offset: 0x000361EC
		public void Set(ref AddNotifyClientAuthStatusChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00037FF8 File Offset: 0x000361F8
		public void Set(ref AddNotifyClientAuthStatusChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00038019 File Offset: 0x00036219
		public void Dispose()
		{
		}

		// Token: 0x04001087 RID: 4231
		private int m_ApiVersion;
	}
}
