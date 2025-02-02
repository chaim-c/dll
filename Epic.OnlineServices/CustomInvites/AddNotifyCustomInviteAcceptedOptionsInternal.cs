using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F1 RID: 1265
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyCustomInviteAcceptedOptionsInternal : ISettable<AddNotifyCustomInviteAcceptedOptions>, IDisposable
	{
		// Token: 0x06002080 RID: 8320 RVA: 0x00030444 File Offset: 0x0002E644
		public void Set(ref AddNotifyCustomInviteAcceptedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00030450 File Offset: 0x0002E650
		public void Set(ref AddNotifyCustomInviteAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00030471 File Offset: 0x0002E671
		public void Dispose()
		{
		}

		// Token: 0x04000E7F RID: 3711
		private int m_ApiVersion;
	}
}
