using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019F RID: 415
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioDevicesChangedOptionsInternal : ISettable<AddNotifyAudioDevicesChangedOptions>, IDisposable
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x00011A7A File Offset: 0x0000FC7A
		public void Set(ref AddNotifyAudioDevicesChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00011A84 File Offset: 0x0000FC84
		public void Set(ref AddNotifyAudioDevicesChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00011AA5 File Offset: 0x0000FCA5
		public void Dispose()
		{
		}

		// Token: 0x0400056C RID: 1388
		private int m_ApiVersion;
	}
}
