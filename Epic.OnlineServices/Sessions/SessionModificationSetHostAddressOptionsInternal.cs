using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000139 RID: 313
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetHostAddressOptionsInternal : ISettable<SessionModificationSetHostAddressOptions>, IDisposable
	{
		// Token: 0x1700020A RID: 522
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0000D91A File Offset: 0x0000BB1A
		public Utf8String HostAddress
		{
			set
			{
				Helper.Set(value, ref this.m_HostAddress);
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000D92A File Offset: 0x0000BB2A
		public void Set(ref SessionModificationSetHostAddressOptions other)
		{
			this.m_ApiVersion = 1;
			this.HostAddress = other.HostAddress;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000D944 File Offset: 0x0000BB44
		public void Set(ref SessionModificationSetHostAddressOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.HostAddress = other.Value.HostAddress;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0000D97A File Offset: 0x0000BB7A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_HostAddress);
		}

		// Token: 0x04000447 RID: 1095
		private int m_ApiVersion;

		// Token: 0x04000448 RID: 1096
		private IntPtr m_HostAddress;
	}
}
