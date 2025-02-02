using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x0200022B RID: 555
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitSnapshotOptionsInternal : ISettable<SubmitSnapshotOptions>, IDisposable
	{
		// Token: 0x17000405 RID: 1029
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00016D9F File Offset: 0x00014F9F
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00016DA9 File Offset: 0x00014FA9
		public void Set(ref SubmitSnapshotOptions other)
		{
			this.m_ApiVersion = 1;
			this.SnapshotId = other.SnapshotId;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00016DC0 File Offset: 0x00014FC0
		public void Set(ref SubmitSnapshotOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.Value.SnapshotId;
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00016DF6 File Offset: 0x00014FF6
		public void Dispose()
		{
		}

		// Token: 0x040006E8 RID: 1768
		private int m_ApiVersion;

		// Token: 0x040006E9 RID: 1769
		private uint m_SnapshotId;
	}
}
