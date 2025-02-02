using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022F RID: 559
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyOnPresenceChangedOptionsInternal : ISettable<AddNotifyOnPresenceChangedOptions>, IDisposable
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x00016E28 File Offset: 0x00015028
		public void Set(ref AddNotifyOnPresenceChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00016E34 File Offset: 0x00015034
		public void Set(ref AddNotifyOnPresenceChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00016E55 File Offset: 0x00015055
		public void Dispose()
		{
		}

		// Token: 0x040006EB RID: 1771
		private int m_ApiVersion;
	}
}
