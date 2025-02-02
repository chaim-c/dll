using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065E RID: 1630
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WindowsRTCOptionsInternal : IGettable<WindowsRTCOptions>, ISettable<WindowsRTCOptions>, IDisposable
	{
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		// (set) Token: 0x060029D2 RID: 10706 RVA: 0x0003E6C9 File Offset: 0x0003C8C9
		public WindowsRTCOptionsPlatformSpecificOptions? PlatformSpecificOptions
		{
			get
			{
				WindowsRTCOptionsPlatformSpecificOptions? result;
				Helper.Get<WindowsRTCOptionsPlatformSpecificOptionsInternal, WindowsRTCOptionsPlatformSpecificOptions>(this.m_PlatformSpecificOptions, out result);
				return result;
			}
			set
			{
				Helper.Set<WindowsRTCOptionsPlatformSpecificOptions, WindowsRTCOptionsPlatformSpecificOptionsInternal>(ref value, ref this.m_PlatformSpecificOptions);
			}
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x0003E6DA File Offset: 0x0003C8DA
		public void Set(ref WindowsRTCOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlatformSpecificOptions = other.PlatformSpecificOptions;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x0003E6F4 File Offset: 0x0003C8F4
		public void Set(ref WindowsRTCOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificOptions = other.Value.PlatformSpecificOptions;
			}
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x0003E72A File Offset: 0x0003C92A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlatformSpecificOptions);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x0003E739 File Offset: 0x0003C939
		public void Get(out WindowsRTCOptions output)
		{
			output = default(WindowsRTCOptions);
			output.Set(ref this);
		}

		// Token: 0x040012FE RID: 4862
		private int m_ApiVersion;

		// Token: 0x040012FF RID: 4863
		private IntPtr m_PlatformSpecificOptions;
	}
}
