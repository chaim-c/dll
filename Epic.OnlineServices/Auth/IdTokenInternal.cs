using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000580 RID: 1408
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IdTokenInternal : IGettable<IdToken>, ISettable<IdToken>, IDisposable
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000359F0 File Offset: 0x00033BF0
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x00035A11 File Offset: 0x00033C11
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

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00035A24 File Offset: 0x00033C24
		// (set) Token: 0x0600240B RID: 9227 RVA: 0x00035A45 File Offset: 0x00033C45
		public Utf8String JsonWebToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_JsonWebToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_JsonWebToken);
			}
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00035A55 File Offset: 0x00033C55
		public void Set(ref IdToken other)
		{
			this.m_ApiVersion = 1;
			this.AccountId = other.AccountId;
			this.JsonWebToken = other.JsonWebToken;
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x00035A7C File Offset: 0x00033C7C
		public void Set(ref IdToken? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.Value.AccountId;
				this.JsonWebToken = other.Value.JsonWebToken;
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00035AC7 File Offset: 0x00033CC7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AccountId);
			Helper.Dispose(ref this.m_JsonWebToken);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00035AE2 File Offset: 0x00033CE2
		public void Get(out IdToken output)
		{
			output = default(IdToken);
			output.Set(ref this);
		}

		// Token: 0x04000FE0 RID: 4064
		private int m_ApiVersion;

		// Token: 0x04000FE1 RID: 4065
		private IntPtr m_AccountId;

		// Token: 0x04000FE2 RID: 4066
		private IntPtr m_JsonWebToken;
	}
}
