using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FB RID: 507
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserTokenByUserIdOptionsInternal : ISettable<CopyUserTokenByUserIdOptions>, IDisposable
	{
		// Token: 0x170003AC RID: 940
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x000154A8 File Offset: 0x000136A8
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170003AD RID: 941
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x000154B8 File Offset: 0x000136B8
		public uint QueryId
		{
			set
			{
				this.m_QueryId = value;
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000154C2 File Offset: 0x000136C2
		public void Set(ref CopyUserTokenByUserIdOptions other)
		{
			this.m_ApiVersion = 2;
			this.TargetUserId = other.TargetUserId;
			this.QueryId = other.QueryId;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000154E8 File Offset: 0x000136E8
		public void Set(ref CopyUserTokenByUserIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.Value.TargetUserId;
				this.QueryId = other.Value.QueryId;
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00015533 File Offset: 0x00013733
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000672 RID: 1650
		private int m_ApiVersion;

		// Token: 0x04000673 RID: 1651
		private IntPtr m_TargetUserId;

		// Token: 0x04000674 RID: 1652
		private uint m_QueryId;
	}
}
