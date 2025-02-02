using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionModificationOptionsInternal : ISettable<UpdateSessionModificationOptions>, IDisposable
	{
		// Token: 0x17000246 RID: 582
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0000F478 File Offset: 0x0000D678
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0000F488 File Offset: 0x0000D688
		public void Set(ref UpdateSessionModificationOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public void Set(ref UpdateSessionModificationOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0000F4D6 File Offset: 0x0000D6D6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x040004BA RID: 1210
		private int m_ApiVersion;

		// Token: 0x040004BB RID: 1211
		private IntPtr m_SessionName;
	}
}
