using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000222 RID: 546
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSnapshotOptionsInternal : ISettable<EndSnapshotOptions>, IDisposable
	{
		// Token: 0x170003FC RID: 1020
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x000169A2 File Offset: 0x00014BA2
		public uint SnapshotId
		{
			set
			{
				this.m_SnapshotId = value;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000169AC File Offset: 0x00014BAC
		public void Set(ref EndSnapshotOptions other)
		{
			this.m_ApiVersion = 1;
			this.SnapshotId = other.SnapshotId;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000169C4 File Offset: 0x00014BC4
		public void Set(ref EndSnapshotOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SnapshotId = other.Value.SnapshotId;
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000169FA File Offset: 0x00014BFA
		public void Dispose()
		{
		}

		// Token: 0x040006D9 RID: 1753
		private int m_ApiVersion;

		// Token: 0x040006DA RID: 1754
		private uint m_SnapshotId;
	}
}
