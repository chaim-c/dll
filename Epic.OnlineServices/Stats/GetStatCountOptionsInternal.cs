using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatCountOptionsInternal : ISettable<GetStatCountOptions>, IDisposable
	{
		// Token: 0x1700011B RID: 283
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00009653 File Offset: 0x00007853
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00009663 File Offset: 0x00007863
		public void Set(ref GetStatCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000967C File Offset: 0x0000787C
		public void Set(ref GetStatCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000096B2 File Offset: 0x000078B2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000304 RID: 772
		private int m_ApiVersion;

		// Token: 0x04000305 RID: 773
		private IntPtr m_TargetUserId;
	}
}
