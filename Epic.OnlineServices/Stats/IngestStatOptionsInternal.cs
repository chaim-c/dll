using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B0 RID: 176
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatOptionsInternal : ISettable<IngestStatOptions>, IDisposable
	{
		// Token: 0x1700012C RID: 300
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00009A66 File Offset: 0x00007C66
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700012D RID: 301
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00009A76 File Offset: 0x00007C76
		public IngestData[] Stats
		{
			set
			{
				Helper.Set<IngestData, IngestDataInternal>(ref value, ref this.m_Stats, out this.m_StatsCount);
			}
		}

		// Token: 0x1700012E RID: 302
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00009A8D File Offset: 0x00007C8D
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00009A9D File Offset: 0x00007C9D
		public void Set(ref IngestStatOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.Stats = other.Stats;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public void Set(ref IngestStatOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.Stats = other.Value.Stats;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00009B30 File Offset: 0x00007D30
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Stats);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000316 RID: 790
		private int m_ApiVersion;

		// Token: 0x04000317 RID: 791
		private IntPtr m_LocalUserId;

		// Token: 0x04000318 RID: 792
		private IntPtr m_Stats;

		// Token: 0x04000319 RID: 793
		private uint m_StatsCount;

		// Token: 0x0400031A RID: 794
		private IntPtr m_TargetUserId;
	}
}
