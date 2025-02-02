using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000128 RID: 296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsGetSessionAttributeCountOptionsInternal : ISettable<SessionDetailsGetSessionAttributeCountOptions>, IDisposable
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0000CA8D File Offset: 0x0000AC8D
		public void Set(ref SessionDetailsGetSessionAttributeCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public void Set(ref SessionDetailsGetSessionAttributeCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000CAB9 File Offset: 0x0000ACB9
		public void Dispose()
		{
		}

		// Token: 0x04000406 RID: 1030
		private int m_ApiVersion;
	}
}
