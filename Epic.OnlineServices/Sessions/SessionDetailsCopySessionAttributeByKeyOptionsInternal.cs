using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000126 RID: 294
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByKeyOptionsInternal : ISettable<SessionDetailsCopySessionAttributeByKeyOptions>, IDisposable
	{
		// Token: 0x170001D8 RID: 472
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0000CA1E File Offset: 0x0000AC1E
		public Utf8String AttrKey
		{
			set
			{
				Helper.Set(value, ref this.m_AttrKey);
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0000CA2E File Offset: 0x0000AC2E
		public void Set(ref SessionDetailsCopySessionAttributeByKeyOptions other)
		{
			this.m_ApiVersion = 1;
			this.AttrKey = other.AttrKey;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000CA48 File Offset: 0x0000AC48
		public void Set(ref SessionDetailsCopySessionAttributeByKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AttrKey = other.Value.AttrKey;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0000CA7E File Offset: 0x0000AC7E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AttrKey);
		}

		// Token: 0x04000404 RID: 1028
		private int m_ApiVersion;

		// Token: 0x04000405 RID: 1029
		private IntPtr m_AttrKey;
	}
}
