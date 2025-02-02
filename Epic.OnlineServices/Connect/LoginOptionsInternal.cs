using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053D RID: 1341
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : ISettable<LoginOptions>, IDisposable
	{
		// Token: 0x17000A18 RID: 2584
		// (set) Token: 0x0600226C RID: 8812 RVA: 0x00033779 File Offset: 0x00031979
		public Credentials? Credentials
		{
			set
			{
				Helper.Set<Credentials, CredentialsInternal>(ref value, ref this.m_Credentials);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x0003378A File Offset: 0x0003198A
		public UserLoginInfo? UserLoginInfo
		{
			set
			{
				Helper.Set<UserLoginInfo, UserLoginInfoInternal>(ref value, ref this.m_UserLoginInfo);
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0003379B File Offset: 0x0003199B
		public void Set(ref LoginOptions other)
		{
			this.m_ApiVersion = 2;
			this.Credentials = other.Credentials;
			this.UserLoginInfo = other.UserLoginInfo;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000337C0 File Offset: 0x000319C0
		public void Set(ref LoginOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Value.Credentials;
				this.UserLoginInfo = other.Value.UserLoginInfo;
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0003380B File Offset: 0x00031A0B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Credentials);
			Helper.Dispose(ref this.m_UserLoginInfo);
		}

		// Token: 0x04000F4E RID: 3918
		private int m_ApiVersion;

		// Token: 0x04000F4F RID: 3919
		private IntPtr m_Credentials;

		// Token: 0x04000F50 RID: 3920
		private IntPtr m_UserLoginInfo;
	}
}
