using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B7 RID: 439
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAudioInputDeviceByIndexOptionsInternal : ISettable<GetAudioInputDeviceByIndexOptions>, IDisposable
	{
		// Token: 0x17000321 RID: 801
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00012D20 File Offset: 0x00010F20
		public uint DeviceInfoIndex
		{
			set
			{
				this.m_DeviceInfoIndex = value;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00012D2A File Offset: 0x00010F2A
		public void Set(ref GetAudioInputDeviceByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.DeviceInfoIndex = other.DeviceInfoIndex;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00012D44 File Offset: 0x00010F44
		public void Set(ref GetAudioInputDeviceByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DeviceInfoIndex = other.Value.DeviceInfoIndex;
			}
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00012D7A File Offset: 0x00010F7A
		public void Dispose()
		{
		}

		// Token: 0x040005B7 RID: 1463
		private int m_ApiVersion;

		// Token: 0x040005B8 RID: 1464
		private uint m_DeviceInfoIndex;
	}
}
