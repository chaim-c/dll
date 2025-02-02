using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000169 RID: 361
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerSanctionCountOptionsInternal : ISettable<GetPlayerSanctionCountOptions>, IDisposable
	{
		// Token: 0x1700024E RID: 590
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0000F633 File Offset: 0x0000D833
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000F643 File Offset: 0x0000D843
		public void Set(ref GetPlayerSanctionCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000F65C File Offset: 0x0000D85C
		public void Set(ref GetPlayerSanctionCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000F692 File Offset: 0x0000D892
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040004C5 RID: 1221
		private int m_ApiVersion;

		// Token: 0x040004C6 RID: 1222
		private IntPtr m_TargetUserId;
	}
}
