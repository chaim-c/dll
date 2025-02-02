using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065A RID: 1626
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RTCOptionsInternal : IGettable<RTCOptions>, ISettable<RTCOptions>, IDisposable
	{
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x0003E188 File Offset: 0x0003C388
		// (set) Token: 0x0600299C RID: 10652 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public IntPtr PlatformSpecificOptions
		{
			get
			{
				return this.m_PlatformSpecificOptions;
			}
			set
			{
				this.m_PlatformSpecificOptions = value;
			}
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x0003E1AA File Offset: 0x0003C3AA
		public void Set(ref RTCOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlatformSpecificOptions = other.PlatformSpecificOptions;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x0003E1C4 File Offset: 0x0003C3C4
		public void Set(ref RTCOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificOptions = other.Value.PlatformSpecificOptions;
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x0003E1FA File Offset: 0x0003C3FA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlatformSpecificOptions);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x0003E209 File Offset: 0x0003C409
		public void Get(out RTCOptions output)
		{
			output = default(RTCOptions);
			output.Set(ref this);
		}

		// Token: 0x040012DE RID: 4830
		private int m_ApiVersion;

		// Token: 0x040012DF RID: 4831
		private IntPtr m_PlatformSpecificOptions;
	}
}
