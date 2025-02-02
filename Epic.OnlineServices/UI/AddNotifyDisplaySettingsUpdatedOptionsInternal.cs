using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000048 RID: 72
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyDisplaySettingsUpdatedOptionsInternal : ISettable<AddNotifyDisplaySettingsUpdatedOptions>, IDisposable
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00006382 File Offset: 0x00004582
		public void Set(ref AddNotifyDisplaySettingsUpdatedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000638C File Offset: 0x0000458C
		public void Set(ref AddNotifyDisplaySettingsUpdatedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000063AD File Offset: 0x000045AD
		public void Dispose()
		{
		}

		// Token: 0x040001B1 RID: 433
		private int m_ApiVersion;
	}
}
