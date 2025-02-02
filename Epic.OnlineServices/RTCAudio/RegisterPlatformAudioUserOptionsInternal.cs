using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D7 RID: 471
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPlatformAudioUserOptionsInternal : ISettable<RegisterPlatformAudioUserOptions>, IDisposable
	{
		// Token: 0x17000332 RID: 818
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x000131B5 File Offset: 0x000113B5
		public Utf8String UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000131C5 File Offset: 0x000113C5
		public void Set(ref RegisterPlatformAudioUserOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000131DC File Offset: 0x000113DC
		public void Set(ref RegisterPlatformAudioUserOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00013212 File Offset: 0x00011412
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x040005CB RID: 1483
		private int m_ApiVersion;

		// Token: 0x040005CC RID: 1484
		private IntPtr m_UserId;
	}
}
