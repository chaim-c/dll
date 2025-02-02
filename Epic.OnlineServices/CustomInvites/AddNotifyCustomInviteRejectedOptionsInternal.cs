using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F5 RID: 1269
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyCustomInviteRejectedOptionsInternal : ISettable<AddNotifyCustomInviteRejectedOptions>, IDisposable
	{
		// Token: 0x06002086 RID: 8326 RVA: 0x000304A4 File Offset: 0x0002E6A4
		public void Set(ref AddNotifyCustomInviteRejectedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000304B0 File Offset: 0x0002E6B0
		public void Set(ref AddNotifyCustomInviteRejectedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000304D1 File Offset: 0x0002E6D1
		public void Dispose()
		{
		}

		// Token: 0x04000E81 RID: 3713
		private int m_ApiVersion;
	}
}
