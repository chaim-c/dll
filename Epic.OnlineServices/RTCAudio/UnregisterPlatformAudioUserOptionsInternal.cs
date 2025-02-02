using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E3 RID: 483
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlatformAudioUserOptionsInternal : ISettable<UnregisterPlatformAudioUserOptions>, IDisposable
	{
		// Token: 0x17000348 RID: 840
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00013E7C File Offset: 0x0001207C
		public Utf8String UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00013E8C File Offset: 0x0001208C
		public void Set(ref UnregisterPlatformAudioUserOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00013EA4 File Offset: 0x000120A4
		public void Set(ref UnregisterPlatformAudioUserOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00013EDA File Offset: 0x000120DA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x0400060C RID: 1548
		private int m_ApiVersion;

		// Token: 0x0400060D RID: 1549
		private IntPtr m_UserId;
	}
}
