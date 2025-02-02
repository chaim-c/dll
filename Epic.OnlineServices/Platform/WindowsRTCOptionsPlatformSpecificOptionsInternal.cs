using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000660 RID: 1632
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsRTCOptionsPlatformSpecificOptionsInternal : IGettable<WindowsRTCOptionsPlatformSpecificOptions>, ISettable<WindowsRTCOptionsPlatformSpecificOptions>, IDisposable
	{
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x0003E76C File Offset: 0x0003C96C
		// (set) Token: 0x060029DB RID: 10715 RVA: 0x0003E78D File Offset: 0x0003C98D
		public Utf8String XAudio29DllPath
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_XAudio29DllPath, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_XAudio29DllPath);
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0003E79D File Offset: 0x0003C99D
		public void Set(ref WindowsRTCOptionsPlatformSpecificOptions other)
		{
			this.m_ApiVersion = 1;
			this.XAudio29DllPath = other.XAudio29DllPath;
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0003E7B4 File Offset: 0x0003C9B4
		public void Set(ref WindowsRTCOptionsPlatformSpecificOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.XAudio29DllPath = other.Value.XAudio29DllPath;
			}
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0003E7EA File Offset: 0x0003C9EA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_XAudio29DllPath);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0003E7F9 File Offset: 0x0003C9F9
		public void Get(out WindowsRTCOptionsPlatformSpecificOptions output)
		{
			output = default(WindowsRTCOptionsPlatformSpecificOptions);
			output.Set(ref this);
		}

		// Token: 0x04001301 RID: 4865
		private int m_ApiVersion;

		// Token: 0x04001302 RID: 4866
		private IntPtr m_XAudio29DllPath;
	}
}
