using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x02000454 RID: 1108
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateIntegratedPlatformOptionsContainerOptionsInternal : ISettable<CreateIntegratedPlatformOptionsContainerOptions>, IDisposable
	{
		// Token: 0x06001C6E RID: 7278 RVA: 0x0002A072 File Offset: 0x00028272
		public void Set(ref CreateIntegratedPlatformOptionsContainerOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0002A07C File Offset: 0x0002827C
		public void Set(ref CreateIntegratedPlatformOptionsContainerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0002A09D File Offset: 0x0002829D
		public void Dispose()
		{
		}

		// Token: 0x04000C90 RID: 3216
		private int m_ApiVersion;
	}
}
