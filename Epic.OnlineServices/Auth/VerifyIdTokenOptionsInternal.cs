using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005AC RID: 1452
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenOptionsInternal : ISettable<VerifyIdTokenOptions>, IDisposable
	{
		// Token: 0x17000B04 RID: 2820
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x00037965 File Offset: 0x00035B65
		public IdToken? IdToken
		{
			set
			{
				Helper.Set<IdToken, IdTokenInternal>(ref value, ref this.m_IdToken);
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00037976 File Offset: 0x00035B76
		public void Set(ref VerifyIdTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.IdToken = other.IdToken;
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00037990 File Offset: 0x00035B90
		public void Set(ref VerifyIdTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.IdToken = other.Value.IdToken;
			}
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000379C6 File Offset: 0x00035BC6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_IdToken);
		}

		// Token: 0x0400106A RID: 4202
		private int m_ApiVersion;

		// Token: 0x0400106B RID: 4203
		private IntPtr m_IdToken;
	}
}
