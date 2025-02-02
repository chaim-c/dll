using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001BB RID: 443
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioOutputDeviceByIndexOptionsInternal : ISettable<GetAudioOutputDeviceByIndexOptions>, IDisposable
	{
		// Token: 0x17000323 RID: 803
		// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00012DBD File Offset: 0x00010FBD
		public uint DeviceInfoIndex
		{
			set
			{
				this.m_DeviceInfoIndex = value;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00012DC7 File Offset: 0x00010FC7
		public void Set(ref GetAudioOutputDeviceByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.DeviceInfoIndex = other.DeviceInfoIndex;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00012DE0 File Offset: 0x00010FE0
		public void Set(ref GetAudioOutputDeviceByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DeviceInfoIndex = other.Value.DeviceInfoIndex;
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00012E16 File Offset: 0x00011016
		public void Dispose()
		{
		}

		// Token: 0x040005BB RID: 1467
		private int m_ApiVersion;

		// Token: 0x040005BC RID: 1468
		private uint m_DeviceInfoIndex;
	}
}
