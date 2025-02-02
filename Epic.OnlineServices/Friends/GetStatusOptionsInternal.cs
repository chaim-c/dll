using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200046B RID: 1131
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatusOptionsInternal : ISettable<GetStatusOptions>, IDisposable
	{
		// Token: 0x17000835 RID: 2101
		// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0002AD37 File Offset: 0x00028F37
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0002AD47 File Offset: 0x00028F47
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0002AD57 File Offset: 0x00028F57
		public void Set(ref GetStatusOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0002AD7C File Offset: 0x00028F7C
		public void Set(ref GetStatusOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0002ADC7 File Offset: 0x00028FC7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000CD4 RID: 3284
		private int m_ApiVersion;

		// Token: 0x04000CD5 RID: 3285
		private IntPtr m_LocalUserId;

		// Token: 0x04000CD6 RID: 3286
		private IntPtr m_TargetUserId;
	}
}
