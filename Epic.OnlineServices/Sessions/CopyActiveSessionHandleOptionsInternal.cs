using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D0 RID: 208
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyActiveSessionHandleOptionsInternal : ISettable<CopyActiveSessionHandleOptions>, IDisposable
	{
		// Token: 0x17000162 RID: 354
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000AD7F File Offset: 0x00008F7F
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000AD8F File Offset: 0x00008F8F
		public void Set(ref CopyActiveSessionHandleOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public void Set(ref CopyActiveSessionHandleOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000ADDE File Offset: 0x00008FDE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x04000368 RID: 872
		private int m_ApiVersion;

		// Token: 0x04000369 RID: 873
		private IntPtr m_SessionName;
	}
}
