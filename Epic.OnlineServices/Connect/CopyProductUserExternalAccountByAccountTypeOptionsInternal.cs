using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000519 RID: 1305
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountTypeOptionsInternal : ISettable<CopyProductUserExternalAccountByAccountTypeOptions>, IDisposable
	{
		// Token: 0x170009C5 RID: 2501
		// (set) Token: 0x06002190 RID: 8592 RVA: 0x00032340 File Offset: 0x00030540
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x00032350 File Offset: 0x00030550
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0003235A File Offset: 0x0003055A
		public void Set(ref CopyProductUserExternalAccountByAccountTypeOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.AccountIdType = other.AccountIdType;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00032380 File Offset: 0x00030580
		public void Set(ref CopyProductUserExternalAccountByAccountTypeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.AccountIdType = other.Value.AccountIdType;
			}
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000323CB File Offset: 0x000305CB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000EF3 RID: 3827
		private int m_ApiVersion;

		// Token: 0x04000EF4 RID: 3828
		private IntPtr m_TargetUserId;

		// Token: 0x04000EF5 RID: 3829
		private ExternalAccountType m_AccountIdType;
	}
}
