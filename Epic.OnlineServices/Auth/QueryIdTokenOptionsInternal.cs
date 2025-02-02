using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A6 RID: 1446
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryIdTokenOptionsInternal : ISettable<QueryIdTokenOptions>, IDisposable
	{
		// Token: 0x17000AD2 RID: 2770
		// (set) Token: 0x06002503 RID: 9475 RVA: 0x00036CBD File Offset: 0x00034EBD
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00036CCD File Offset: 0x00034ECD
		public EpicAccountId TargetAccountId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetAccountId);
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00036CDD File Offset: 0x00034EDD
		public void Set(ref QueryIdTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetAccountId = other.TargetAccountId;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x00036D04 File Offset: 0x00034F04
		public void Set(ref QueryIdTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetAccountId = other.Value.TargetAccountId;
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00036D4F File Offset: 0x00034F4F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetAccountId);
		}

		// Token: 0x04001037 RID: 4151
		private int m_ApiVersion;

		// Token: 0x04001038 RID: 4152
		private IntPtr m_LocalUserId;

		// Token: 0x04001039 RID: 4153
		private IntPtr m_TargetAccountId;
	}
}
