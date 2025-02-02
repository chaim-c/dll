using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000237 RID: 567
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetJoinInfoOptionsInternal : ISettable<GetJoinInfoOptions>, IDisposable
	{
		// Token: 0x17000412 RID: 1042
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0001710A File Offset: 0x0001530A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0001711A File Offset: 0x0001531A
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0001712A File Offset: 0x0001532A
		public void Set(ref GetJoinInfoOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00017150 File Offset: 0x00015350
		public void Set(ref GetJoinInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0001719B File Offset: 0x0001539B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040006FB RID: 1787
		private int m_ApiVersion;

		// Token: 0x040006FC RID: 1788
		private IntPtr m_LocalUserId;

		// Token: 0x040006FD RID: 1789
		private IntPtr m_TargetUserId;
	}
}
