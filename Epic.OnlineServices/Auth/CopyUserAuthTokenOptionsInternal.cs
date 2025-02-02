using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000578 RID: 1400
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserAuthTokenOptionsInternal : ISettable<CopyUserAuthTokenOptions>, IDisposable
	{
		// Token: 0x060023D2 RID: 9170 RVA: 0x00035515 File Offset: 0x00033715
		public void Set(ref CopyUserAuthTokenOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00035520 File Offset: 0x00033720
		public void Set(ref CopyUserAuthTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00035541 File Offset: 0x00033741
		public void Dispose()
		{
		}

		// Token: 0x04000FCB RID: 4043
		private int m_ApiVersion;
	}
}
