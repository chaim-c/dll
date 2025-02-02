using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B0 RID: 1456
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthOptionsInternal : ISettable<VerifyUserAuthOptions>, IDisposable
	{
		// Token: 0x17000B0B RID: 2827
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00037B3A File Offset: 0x00035D3A
		public Token? AuthToken
		{
			set
			{
				Helper.Set<Token, TokenInternal>(ref value, ref this.m_AuthToken);
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00037B4B File Offset: 0x00035D4B
		public void Set(ref VerifyUserAuthOptions other)
		{
			this.m_ApiVersion = 1;
			this.AuthToken = other.AuthToken;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00037B64 File Offset: 0x00035D64
		public void Set(ref VerifyUserAuthOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AuthToken = other.Value.AuthToken;
			}
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00037B9A File Offset: 0x00035D9A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AuthToken);
		}

		// Token: 0x04001071 RID: 4209
		private int m_ApiVersion;

		// Token: 0x04001072 RID: 4210
		private IntPtr m_AuthToken;
	}
}
