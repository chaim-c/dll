using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B6 RID: 1462
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSLoginOptionsInternal : ISettable<IOSLoginOptions>, IDisposable
	{
		// Token: 0x17000B1A RID: 2842
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x00037F25 File Offset: 0x00036125
		public IOSCredentials? Credentials
		{
			set
			{
				Helper.Set<IOSCredentials, IOSCredentialsInternal>(ref value, ref this.m_Credentials);
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x00037F36 File Offset: 0x00036136
		public AuthScopeFlags ScopeFlags
		{
			set
			{
				this.m_ScopeFlags = value;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x00037F40 File Offset: 0x00036140
		public void Set(ref IOSLoginOptions other)
		{
			this.m_ApiVersion = 2;
			this.Credentials = other.Credentials;
			this.ScopeFlags = other.ScopeFlags;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x00037F64 File Offset: 0x00036164
		public void Set(ref IOSLoginOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Value.Credentials;
				this.ScopeFlags = other.Value.ScopeFlags;
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x00037FAF File Offset: 0x000361AF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Credentials);
		}

		// Token: 0x04001083 RID: 4227
		private int m_ApiVersion;

		// Token: 0x04001084 RID: 4228
		private IntPtr m_Credentials;

		// Token: 0x04001085 RID: 4229
		private AuthScopeFlags m_ScopeFlags;
	}
}
