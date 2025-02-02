using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000135 RID: 309
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationRemoveAttributeOptionsInternal : ISettable<SessionModificationRemoveAttributeOptions>, IDisposable
	{
		// Token: 0x17000206 RID: 518
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0000D81B File Offset: 0x0000BA1B
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000D82B File Offset: 0x0000BA2B
		public void Set(ref SessionModificationRemoveAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0000D844 File Offset: 0x0000BA44
		public void Set(ref SessionModificationRemoveAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0000D87A File Offset: 0x0000BA7A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x04000441 RID: 1089
		private int m_ApiVersion;

		// Token: 0x04000442 RID: 1090
		private IntPtr m_Key;
	}
}
