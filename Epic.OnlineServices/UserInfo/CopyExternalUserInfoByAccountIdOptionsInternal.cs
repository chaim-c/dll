using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000025 RID: 37
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountIdOptionsInternal : ISettable<CopyExternalUserInfoByAccountIdOptions>, IDisposable
	{
		// Token: 0x17000015 RID: 21
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00004A1C File Offset: 0x00002C1C
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00004A2C File Offset: 0x00002C2C
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00004A3C File Offset: 0x00002C3C
		public Utf8String AccountId
		{
			set
			{
				Helper.Set(value, ref this.m_AccountId);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00004A4C File Offset: 0x00002C4C
		public void Set(ref CopyExternalUserInfoByAccountIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.AccountId = other.AccountId;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00004A80 File Offset: 0x00002C80
		public void Set(ref CopyExternalUserInfoByAccountIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.AccountId = other.Value.AccountId;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_AccountId);
		}

		// Token: 0x04000140 RID: 320
		private int m_ApiVersion;

		// Token: 0x04000141 RID: 321
		private IntPtr m_LocalUserId;

		// Token: 0x04000142 RID: 322
		private IntPtr m_TargetUserId;

		// Token: 0x04000143 RID: 323
		private IntPtr m_AccountId;
	}
}
