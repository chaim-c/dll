using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A8 RID: 1448
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TokenInternal : IGettable<Token>, ISettable<Token>, IDisposable
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x00036EA4 File Offset: 0x000350A4
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x00036EC5 File Offset: 0x000350C5
		public Utf8String App
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_App, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_App);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x00036ED8 File Offset: 0x000350D8
		// (set) Token: 0x06002520 RID: 9504 RVA: 0x00036EF9 File Offset: 0x000350F9
		public Utf8String ClientId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ClientId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientId);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002521 RID: 9505 RVA: 0x00036F0C File Offset: 0x0003510C
		// (set) Token: 0x06002522 RID: 9506 RVA: 0x00036F2D File Offset: 0x0003512D
		public EpicAccountId AccountId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x00036F40 File Offset: 0x00035140
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x00036F61 File Offset: 0x00035161
		public Utf8String AccessToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_AccessToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AccessToken);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00036F74 File Offset: 0x00035174
		// (set) Token: 0x06002526 RID: 9510 RVA: 0x00036F8C File Offset: 0x0003518C
		public double ExpiresIn
		{
			get
			{
				return this.m_ExpiresIn;
			}
			set
			{
				this.m_ExpiresIn = value;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00036F98 File Offset: 0x00035198
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x00036FB9 File Offset: 0x000351B9
		public Utf8String ExpiresAt
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ExpiresAt, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ExpiresAt);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x00036FCC File Offset: 0x000351CC
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x00036FE4 File Offset: 0x000351E4
		public AuthTokenType AuthType
		{
			get
			{
				return this.m_AuthType;
			}
			set
			{
				this.m_AuthType = value;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x00036FF0 File Offset: 0x000351F0
		// (set) Token: 0x0600252C RID: 9516 RVA: 0x00037011 File Offset: 0x00035211
		public Utf8String RefreshToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_RefreshToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RefreshToken);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x00037024 File Offset: 0x00035224
		// (set) Token: 0x0600252E RID: 9518 RVA: 0x0003703C File Offset: 0x0003523C
		public double RefreshExpiresIn
		{
			get
			{
				return this.m_RefreshExpiresIn;
			}
			set
			{
				this.m_RefreshExpiresIn = value;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x00037048 File Offset: 0x00035248
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x00037069 File Offset: 0x00035269
		public Utf8String RefreshExpiresAt
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_RefreshExpiresAt, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_RefreshExpiresAt);
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0003707C File Offset: 0x0003527C
		public void Set(ref Token other)
		{
			this.m_ApiVersion = 2;
			this.App = other.App;
			this.ClientId = other.ClientId;
			this.AccountId = other.AccountId;
			this.AccessToken = other.AccessToken;
			this.ExpiresIn = other.ExpiresIn;
			this.ExpiresAt = other.ExpiresAt;
			this.AuthType = other.AuthType;
			this.RefreshToken = other.RefreshToken;
			this.RefreshExpiresIn = other.RefreshExpiresIn;
			this.RefreshExpiresAt = other.RefreshExpiresAt;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00037114 File Offset: 0x00035314
		public void Set(ref Token? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.App = other.Value.App;
				this.ClientId = other.Value.ClientId;
				this.AccountId = other.Value.AccountId;
				this.AccessToken = other.Value.AccessToken;
				this.ExpiresIn = other.Value.ExpiresIn;
				this.ExpiresAt = other.Value.ExpiresAt;
				this.AuthType = other.Value.AuthType;
				this.RefreshToken = other.Value.RefreshToken;
				this.RefreshExpiresIn = other.Value.RefreshExpiresIn;
				this.RefreshExpiresAt = other.Value.RefreshExpiresAt;
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0003720C File Offset: 0x0003540C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_App);
			Helper.Dispose(ref this.m_ClientId);
			Helper.Dispose(ref this.m_AccountId);
			Helper.Dispose(ref this.m_AccessToken);
			Helper.Dispose(ref this.m_ExpiresAt);
			Helper.Dispose(ref this.m_RefreshToken);
			Helper.Dispose(ref this.m_RefreshExpiresAt);
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0003726E File Offset: 0x0003546E
		public void Get(out Token output)
		{
			output = default(Token);
			output.Set(ref this);
		}

		// Token: 0x04001044 RID: 4164
		private int m_ApiVersion;

		// Token: 0x04001045 RID: 4165
		private IntPtr m_App;

		// Token: 0x04001046 RID: 4166
		private IntPtr m_ClientId;

		// Token: 0x04001047 RID: 4167
		private IntPtr m_AccountId;

		// Token: 0x04001048 RID: 4168
		private IntPtr m_AccessToken;

		// Token: 0x04001049 RID: 4169
		private double m_ExpiresIn;

		// Token: 0x0400104A RID: 4170
		private IntPtr m_ExpiresAt;

		// Token: 0x0400104B RID: 4171
		private AuthTokenType m_AuthType;

		// Token: 0x0400104C RID: 4172
		private IntPtr m_RefreshToken;

		// Token: 0x0400104D RID: 4173
		private double m_RefreshExpiresIn;

		// Token: 0x0400104E RID: 4174
		private IntPtr m_RefreshExpiresAt;
	}
}
