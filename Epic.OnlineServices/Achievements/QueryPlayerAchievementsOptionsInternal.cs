using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069B RID: 1691
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPlayerAchievementsOptionsInternal : ISettable<QueryPlayerAchievementsOptions>, IDisposable
	{
		// Token: 0x17000D2F RID: 3375
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000411E1 File Offset: 0x0003F3E1
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (set) Token: 0x06002B7F RID: 11135 RVA: 0x000411F1 File Offset: 0x0003F3F1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00041201 File Offset: 0x0003F401
		public void Set(ref QueryPlayerAchievementsOptions other)
		{
			this.m_ApiVersion = 2;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x00041228 File Offset: 0x0003F428
		public void Set(ref QueryPlayerAchievementsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x00041273 File Offset: 0x0003F473
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040013BC RID: 5052
		private int m_ApiVersion;

		// Token: 0x040013BD RID: 5053
		private IntPtr m_TargetUserId;

		// Token: 0x040013BE RID: 5054
		private IntPtr m_LocalUserId;
	}
}
