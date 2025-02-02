using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000615 RID: 1557
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyMessageToPeerOptionsInternal : ISettable<AddNotifyMessageToPeerOptions>, IDisposable
	{
		// Token: 0x060027E9 RID: 10217 RVA: 0x0003B774 File Offset: 0x00039974
		public void Set(ref AddNotifyMessageToPeerOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0003B780 File Offset: 0x00039980
		public void Set(ref AddNotifyMessageToPeerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0003B7A1 File Offset: 0x000399A1
		public void Dispose()
		{
		}

		// Token: 0x040011F0 RID: 4592
		private int m_ApiVersion;
	}
}
