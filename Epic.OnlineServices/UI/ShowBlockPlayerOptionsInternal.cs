using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000072 RID: 114
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowBlockPlayerOptionsInternal : ISettable<ShowBlockPlayerOptions>, IDisposable
	{
		// Token: 0x170000A5 RID: 165
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000709B File Offset: 0x0000529B
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x000070AB File Offset: 0x000052AB
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000070BB File Offset: 0x000052BB
		public void Set(ref ShowBlockPlayerOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000070E0 File Offset: 0x000052E0
		public void Set(ref ShowBlockPlayerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000712B File Offset: 0x0000532B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400025F RID: 607
		private int m_ApiVersion;

		// Token: 0x04000260 RID: 608
		private IntPtr m_LocalUserId;

		// Token: 0x04000261 RID: 609
		private IntPtr m_TargetUserId;
	}
}
