using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200057E RID: 1406
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthOptionsInternal : ISettable<DeletePersistentAuthOptions>, IDisposable
	{
		// Token: 0x17000A84 RID: 2692
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x00035942 File Offset: 0x00033B42
		public Utf8String RefreshToken
		{
			set
			{
				Helper.Set(value, ref this.m_RefreshToken);
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00035952 File Offset: 0x00033B52
		public void Set(ref DeletePersistentAuthOptions other)
		{
			this.m_ApiVersion = 2;
			this.RefreshToken = other.RefreshToken;
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0003596C File Offset: 0x00033B6C
		public void Set(ref DeletePersistentAuthOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.RefreshToken = other.Value.RefreshToken;
			}
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000359A2 File Offset: 0x00033BA2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_RefreshToken);
		}

		// Token: 0x04000FDC RID: 4060
		private int m_ApiVersion;

		// Token: 0x04000FDD RID: 4061
		private IntPtr m_RefreshToken;
	}
}
