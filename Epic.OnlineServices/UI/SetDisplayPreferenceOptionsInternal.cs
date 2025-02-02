using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200006E RID: 110
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetDisplayPreferenceOptionsInternal : ISettable<SetDisplayPreferenceOptions>, IDisposable
	{
		// Token: 0x170000A0 RID: 160
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00006FB0 File Offset: 0x000051B0
		public NotificationLocation NotificationLocation
		{
			set
			{
				this.m_NotificationLocation = value;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00006FBA File Offset: 0x000051BA
		public void Set(ref SetDisplayPreferenceOptions other)
		{
			this.m_ApiVersion = 1;
			this.NotificationLocation = other.NotificationLocation;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00006FD4 File Offset: 0x000051D4
		public void Set(ref SetDisplayPreferenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.NotificationLocation = other.Value.NotificationLocation;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000700A File Offset: 0x0000520A
		public void Dispose()
		{
		}

		// Token: 0x04000258 RID: 600
		private int m_ApiVersion;

		// Token: 0x04000259 RID: 601
		private NotificationLocation m_NotificationLocation;
	}
}
