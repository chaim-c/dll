using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200057A RID: 1402
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : IGettable<Credentials>, ISettable<Credentials>, IDisposable
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060023E0 RID: 9184 RVA: 0x000355EC File Offset: 0x000337EC
		// (set) Token: 0x060023E1 RID: 9185 RVA: 0x0003560D File Offset: 0x0003380D
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

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x00035620 File Offset: 0x00033820
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x00035641 File Offset: 0x00033841
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00035654 File Offset: 0x00033854
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x0003566C File Offset: 0x0003386C
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

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060023E6 RID: 9190 RVA: 0x00035678 File Offset: 0x00033878
		// (set) Token: 0x060023E7 RID: 9191 RVA: 0x00035690 File Offset: 0x00033890
		public IntPtr SystemAuthCredentialsOptions
		{
			get
			{
				return this.m_SystemAuthCredentialsOptions;
			}
			set
			{
				this.m_SystemAuthCredentialsOptions = value;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060023E8 RID: 9192 RVA: 0x0003569C File Offset: 0x0003389C
		// (set) Token: 0x060023E9 RID: 9193 RVA: 0x000356B4 File Offset: 0x000338B4
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

		// Token: 0x060023EA RID: 9194 RVA: 0x000356C0 File Offset: 0x000338C0
		public void Set(ref Credentials other)
		{
			this.m_ApiVersion = 3;
			this.Id = other.Id;
			this.Token = other.Token;
			this.Type = other.Type;
			this.SystemAuthCredentialsOptions = other.SystemAuthCredentialsOptions;
			this.ExternalType = other.ExternalType;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x00035718 File Offset: 0x00033918
		public void Set(ref Credentials? other)
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

		// Token: 0x060023EC RID: 9196 RVA: 0x000357A2 File Offset: 0x000339A2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Id);
			Helper.Dispose(ref this.m_Token);
			Helper.Dispose(ref this.m_SystemAuthCredentialsOptions);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000357C9 File Offset: 0x000339C9
		public void Get(out Credentials output)
		{
			output = default(Credentials);
			output.Set(ref this);
		}

		// Token: 0x04000FD1 RID: 4049
		private int m_ApiVersion;

		// Token: 0x04000FD2 RID: 4050
		private IntPtr m_Id;

		// Token: 0x04000FD3 RID: 4051
		private IntPtr m_Token;

		// Token: 0x04000FD4 RID: 4052
		private LoginCredentialType m_Type;

		// Token: 0x04000FD5 RID: 4053
		private IntPtr m_SystemAuthCredentialsOptions;

		// Token: 0x04000FD6 RID: 4054
		private ExternalCredentialType m_ExternalType;
	}
}
