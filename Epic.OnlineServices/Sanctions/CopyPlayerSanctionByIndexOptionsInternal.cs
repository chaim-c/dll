using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerSanctionByIndexOptionsInternal : ISettable<CopyPlayerSanctionByIndexOptions>, IDisposable
	{
		// Token: 0x1700024B RID: 587
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0000F587 File Offset: 0x0000D787
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x1700024C RID: 588
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0000F597 File Offset: 0x0000D797
		public uint SanctionIndex
		{
			set
			{
				this.m_SanctionIndex = value;
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000F5A1 File Offset: 0x0000D7A1
		public void Set(ref CopyPlayerSanctionByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.SanctionIndex = other.SanctionIndex;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public void Set(ref CopyPlayerSanctionByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.SanctionIndex = other.Value.SanctionIndex;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000F613 File Offset: 0x0000D813
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040004C1 RID: 1217
		private int m_ApiVersion;

		// Token: 0x040004C2 RID: 1218
		private IntPtr m_TargetUserId;

		// Token: 0x040004C3 RID: 1219
		private uint m_SanctionIndex;
	}
}
