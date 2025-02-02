using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000517 RID: 1303
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountIdOptionsInternal : ISettable<CopyProductUserExternalAccountByAccountIdOptions>, IDisposable
	{
		// Token: 0x170009C1 RID: 2497
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x00032273 File Offset: 0x00030473
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x00032283 File Offset: 0x00030483
		public Utf8String AccountId
		{
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00032293 File Offset: 0x00030493
		public void Set(ref CopyProductUserExternalAccountByAccountIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.AccountId = other.AccountId;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000322B8 File Offset: 0x000304B8
		public void Set(ref CopyProductUserExternalAccountByAccountIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.AccountId = other.Value.AccountId;
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00032303 File Offset: 0x00030503
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_AccountId);
		}

		// Token: 0x04000EEE RID: 3822
		private int m_ApiVersion;

		// Token: 0x04000EEF RID: 3823
		private IntPtr m_TargetUserId;

		// Token: 0x04000EF0 RID: 3824
		private IntPtr m_AccountId;
	}
}
