using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B2 RID: 1458
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSCredentialsInternal : IGettable<IOSCredentials>, ISettable<IOSCredentials>, IDisposable
	{
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x00037C50 File Offset: 0x00035E50
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x00037C71 File Offset: 0x00035E71
		public Utf8String Id
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Id);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x00037C84 File Offset: 0x00035E84
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x00037CA5 File Offset: 0x00035EA5
		public Utf8String Token
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Token);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x00037CB8 File Offset: 0x00035EB8
		// (set) Token: 0x0600259B RID: 9627 RVA: 0x00037CD0 File Offset: 0x00035ED0
		public LoginCredentialType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x00037CDC File Offset: 0x00035EDC
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x00037CFD File Offset: 0x00035EFD
		public IOSCredentialsSystemAuthCredentialsOptions? SystemAuthCredentialsOptions
		{
			get
			{
				IOSCredentialsSystemAuthCredentialsOptions? result;
				Helper.Get<IOSCredentialsSystemAuthCredentialsOptionsInternal, IOSCredentialsSystemAuthCredentialsOptions>(this.m_SystemAuthCredentialsOptions, out result);
				return result;
			}
			set
			{
				Helper.Set<IOSCredentialsSystemAuthCredentialsOptions, IOSCredentialsSystemAuthCredentialsOptionsInternal>(ref value, ref this.m_SystemAuthCredentialsOptions);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x00037D10 File Offset: 0x00035F10
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x00037D28 File Offset: 0x00035F28
		public ExternalCredentialType ExternalType
		{
			get
			{
				return this.m_ExternalType;
			}
			set
			{
				this.m_ExternalType = value;
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x00037D34 File Offset: 0x00035F34
		public void Set(ref IOSCredentials other)
		{
			this.m_ApiVersion = 3;
			this.Id = other.Id;
			this.Token = other.Token;
			this.Type = other.Type;
			this.SystemAuthCredentialsOptions = other.SystemAuthCredentialsOptions;
			this.ExternalType = other.ExternalType;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00037D8C File Offset: 0x00035F8C
		public void Set(ref IOSCredentials? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.Id = other.Value.Id;
				this.Token = other.Value.Token;
				this.Type = other.Value.Type;
				this.SystemAuthCredentialsOptions = other.Value.SystemAuthCredentialsOptions;
				this.ExternalType = other.Value.ExternalType;
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00037E16 File Offset: 0x00036016
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Id);
			Helper.Dispose(ref this.m_Token);
			Helper.Dispose(ref this.m_SystemAuthCredentialsOptions);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00037E3D File Offset: 0x0003603D
		public void Get(out IOSCredentials output)
		{
			output = default(IOSCredentials);
			output.Set(ref this);
		}

		// Token: 0x04001078 RID: 4216
		private int m_ApiVersion;

		// Token: 0x04001079 RID: 4217
		private IntPtr m_Id;

		// Token: 0x0400107A RID: 4218
		private IntPtr m_Token;

		// Token: 0x0400107B RID: 4219
		private LoginCredentialType m_Type;

		// Token: 0x0400107C RID: 4220
		private IntPtr m_SystemAuthCredentialsOptions;

		// Token: 0x0400107D RID: 4221
		private ExternalCredentialType m_ExternalType;
	}
}
