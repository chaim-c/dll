using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200052B RID: 1323
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdOptionsInternal : ISettable<DeleteDeviceIdOptions>, IDisposable
	{
		// Token: 0x060021EE RID: 8686 RVA: 0x00032BB1 File Offset: 0x00030DB1
		public void Set(ref DeleteDeviceIdOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x00032BBC File Offset: 0x00030DBC
		public void Set(ref DeleteDeviceIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00032BDD File Offset: 0x00030DDD
		public void Dispose()
		{
		}

		// Token: 0x04000F17 RID: 3863
		private int m_ApiVersion;
	}
}
