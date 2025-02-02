using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200050E RID: 1294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAuthExpirationOptionsInternal : ISettable<AddNotifyAuthExpirationOptions>, IDisposable
	{
		// Token: 0x06002141 RID: 8513 RVA: 0x00031642 File Offset: 0x0002F842
		public void Set(ref AddNotifyAuthExpirationOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0003164C File Offset: 0x0002F84C
		public void Set(ref AddNotifyAuthExpirationOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0003166D File Offset: 0x0002F86D
		public void Dispose()
		{
		}

		// Token: 0x04000EC4 RID: 3780
		private int m_ApiVersion;
	}
}
