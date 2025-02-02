using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200058A RID: 1418
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : ISettable<LoginOptions>, IDisposable
	{
		// Token: 0x17000AAB RID: 2731
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x0003630A File Offset: 0x0003450A
		public Credentials? Credentials
		{
			set
			{
				Helper.Set<Credentials, CredentialsInternal>(ref value, ref this.m_Credentials);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0003631B File Offset: 0x0003451B
		public AuthScopeFlags ScopeFlags
		{
			set
			{
				this.m_ScopeFlags = value;
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00036325 File Offset: 0x00034525
		public void Set(ref LoginOptions other)
		{
			this.m_ApiVersion = 2;
			this.Credentials = other.Credentials;
			this.ScopeFlags = other.ScopeFlags;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0003634C File Offset: 0x0003454C
		public void Set(ref LoginOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Value.Credentials;
				this.ScopeFlags = other.Value.ScopeFlags;
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00036397 File Offset: 0x00034597
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Credentials);
		}

		// Token: 0x04001010 RID: 4112
		private int m_ApiVersion;

		// Token: 0x04001011 RID: 4113
		private IntPtr m_Credentials;

		// Token: 0x04001012 RID: 4114
		private AuthScopeFlags m_ScopeFlags;
	}
}
