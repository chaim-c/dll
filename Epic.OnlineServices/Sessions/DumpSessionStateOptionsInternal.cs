using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E0 RID: 224
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DumpSessionStateOptionsInternal : ISettable<DumpSessionStateOptions>, IDisposable
	{
		// Token: 0x17000181 RID: 385
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000B3E6 File Offset: 0x000095E6
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000B3F6 File Offset: 0x000095F6
		public void Set(ref DumpSessionStateOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000B410 File Offset: 0x00009610
		public void Set(ref DumpSessionStateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000B446 File Offset: 0x00009646
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x0400038D RID: 909
		private int m_ApiVersion;

		// Token: 0x0400038E RID: 910
		private IntPtr m_SessionName;
	}
}
