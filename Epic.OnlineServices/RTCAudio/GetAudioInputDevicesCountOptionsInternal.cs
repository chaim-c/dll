using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B9 RID: 441
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioInputDevicesCountOptionsInternal : ISettable<GetAudioInputDevicesCountOptions>, IDisposable
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x00012D7D File Offset: 0x00010F7D
		public void Set(ref GetAudioInputDevicesCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00012D88 File Offset: 0x00010F88
		public void Set(ref GetAudioInputDevicesCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00012DA9 File Offset: 0x00010FA9
		public void Dispose()
		{
		}

		// Token: 0x040005B9 RID: 1465
		private int m_ApiVersion;
	}
}
