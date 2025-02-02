using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000165 RID: 357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionOptionsInternal : ISettable<UpdateSessionOptions>, IDisposable
	{
		// Token: 0x17000248 RID: 584
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0000F4F6 File Offset: 0x0000D6F6
		public SessionModification SessionModificationHandle
		{
			set
			{
				Helper.Set(value, ref this.m_SessionModificationHandle);
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000F506 File Offset: 0x0000D706
		public void Set(ref UpdateSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionModificationHandle = other.SessionModificationHandle;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000F520 File Offset: 0x0000D720
		public void Set(ref UpdateSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionModificationHandle = other.Value.SessionModificationHandle;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000F556 File Offset: 0x0000D756
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionModificationHandle);
		}

		// Token: 0x040004BD RID: 1213
		private int m_ApiVersion;

		// Token: 0x040004BE RID: 1214
		private IntPtr m_SessionModificationHandle;
	}
}
