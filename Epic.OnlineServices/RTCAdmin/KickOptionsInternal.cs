using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FF RID: 511
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickOptionsInternal : ISettable<KickOptions>, IDisposable
	{
		// Token: 0x170003B5 RID: 949
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x000156B7 File Offset: 0x000138B7
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x000156C7 File Offset: 0x000138C7
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000156D7 File Offset: 0x000138D7
		public void Set(ref KickOptions other)
		{
			this.m_ApiVersion = 1;
			this.RoomName = other.RoomName;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000156FC File Offset: 0x000138FC
		public void Set(ref KickOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.RoomName = other.Value.RoomName;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00015747 File Offset: 0x00013947
		public void Dispose()
		{
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400067B RID: 1659
		private int m_ApiVersion;

		// Token: 0x0400067C RID: 1660
		private IntPtr m_RoomName;

		// Token: 0x0400067D RID: 1661
		private IntPtr m_TargetUserId;
	}
}
