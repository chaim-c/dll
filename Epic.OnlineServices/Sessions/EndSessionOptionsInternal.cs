using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable<EndSessionOptions>, IDisposable
	{
		// Token: 0x17000188 RID: 392
		// (set) Token: 0x06000784 RID: 1924 RVA: 0x0000B5BA File Offset: 0x000097BA
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000B5CA File Offset: 0x000097CA
		public void Set(ref EndSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000B5E4 File Offset: 0x000097E4
		public void Set(ref EndSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000B61A File Offset: 0x0000981A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x04000394 RID: 916
		private int m_ApiVersion;

		// Token: 0x04000395 RID: 917
		private IntPtr m_SessionName;
	}
}
