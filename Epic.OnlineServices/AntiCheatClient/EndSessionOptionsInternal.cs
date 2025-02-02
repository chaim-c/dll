using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000622 RID: 1570
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable<EndSessionOptions>, IDisposable
	{
		// Token: 0x0600281A RID: 10266 RVA: 0x0003BF62 File Offset: 0x0003A162
		public void Set(ref EndSessionOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0003BF6C File Offset: 0x0003A16C
		public void Set(ref EndSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0003BF8D File Offset: 0x0003A18D
		public void Dispose()
		{
		}

		// Token: 0x04001221 RID: 4641
		private int m_ApiVersion;
	}
}
