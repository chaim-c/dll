using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001BD RID: 445
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioOutputDevicesCountOptionsInternal : ISettable<GetAudioOutputDevicesCountOptions>, IDisposable
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x00012E19 File Offset: 0x00011019
		public void Set(ref GetAudioOutputDevicesCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00012E24 File Offset: 0x00011024
		public void Set(ref GetAudioOutputDevicesCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00012E45 File Offset: 0x00011045
		public void Dispose()
		{
		}

		// Token: 0x040005BD RID: 1469
		private int m_ApiVersion;
	}
}
