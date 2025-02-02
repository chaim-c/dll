using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000029 RID: 41
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByIndexOptionsInternal : ISettable<CopyExternalUserInfoByIndexOptions>, IDisposable
	{
		// Token: 0x17000021 RID: 33
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00004C46 File Offset: 0x00002E46
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000022 RID: 34
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00004C56 File Offset: 0x00002E56
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00004C66 File Offset: 0x00002E66
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00004C70 File Offset: 0x00002E70
		public void Set(ref CopyExternalUserInfoByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.Index = other.Index;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public void Set(ref CopyExternalUserInfoByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00004D04 File Offset: 0x00002F04
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400014E RID: 334
		private int m_ApiVersion;

		// Token: 0x0400014F RID: 335
		private IntPtr m_LocalUserId;

		// Token: 0x04000150 RID: 336
		private IntPtr m_TargetUserId;

		// Token: 0x04000151 RID: 337
		private uint m_Index;
	}
}
