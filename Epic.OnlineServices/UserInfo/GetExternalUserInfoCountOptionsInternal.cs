using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002F RID: 47
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalUserInfoCountOptionsInternal : ISettable<GetExternalUserInfoCountOptions>, IDisposable
	{
		// Token: 0x17000032 RID: 50
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00005038 File Offset: 0x00003238
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00005048 File Offset: 0x00003248
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00005058 File Offset: 0x00003258
		public void Set(ref GetExternalUserInfoCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000507C File Offset: 0x0000327C
		public void Set(ref GetExternalUserInfoCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000050C7 File Offset: 0x000032C7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000162 RID: 354
		private int m_ApiVersion;

		// Token: 0x04000163 RID: 355
		private IntPtr m_LocalUserId;

		// Token: 0x04000164 RID: 356
		private IntPtr m_TargetUserId;
	}
}
