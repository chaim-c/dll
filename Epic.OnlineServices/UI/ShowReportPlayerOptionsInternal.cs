using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000078 RID: 120
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowReportPlayerOptionsInternal : ISettable<ShowReportPlayerOptions>, IDisposable
	{
		// Token: 0x170000B2 RID: 178
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000073B7 File Offset: 0x000055B7
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x000073C7 File Offset: 0x000055C7
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000073D7 File Offset: 0x000055D7
		public void Set(ref ShowReportPlayerOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000073FC File Offset: 0x000055FC
		public void Set(ref ShowReportPlayerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00007447 File Offset: 0x00005647
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400026D RID: 621
		private int m_ApiVersion;

		// Token: 0x0400026E RID: 622
		private IntPtr m_LocalUserId;

		// Token: 0x0400026F RID: 623
		private IntPtr m_TargetUserId;
	}
}
