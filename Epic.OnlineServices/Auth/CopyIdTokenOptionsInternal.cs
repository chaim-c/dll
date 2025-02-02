using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000576 RID: 1398
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyIdTokenOptionsInternal : ISettable<CopyIdTokenOptions>, IDisposable
	{
		// Token: 0x17000A73 RID: 2675
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x000354A7 File Offset: 0x000336A7
		public EpicAccountId AccountId
		{
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000354B7 File Offset: 0x000336B7
		public void Set(ref CopyIdTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.AccountId = other.AccountId;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000354D0 File Offset: 0x000336D0
		public void Set(ref CopyIdTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.Value.AccountId;
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00035506 File Offset: 0x00033706
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AccountId);
		}

		// Token: 0x04000FC9 RID: 4041
		private int m_ApiVersion;

		// Token: 0x04000FCA RID: 4042
		private IntPtr m_AccountId;
	}
}
