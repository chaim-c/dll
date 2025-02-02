using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F7 RID: 503
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingVolumeOptionsInternal : ISettable<UpdateSendingVolumeOptions>, IDisposable
	{
		// Token: 0x170003A3 RID: 931
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00015303 File Offset: 0x00013503
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x00015313 File Offset: 0x00013513
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00015323 File Offset: 0x00013523
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001532D File Offset: 0x0001352D
		public void Set(ref UpdateSendingVolumeOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00015360 File Offset: 0x00013560
		public void Set(ref UpdateSendingVolumeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Volume = other.Value.Volume;
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000153C0 File Offset: 0x000135C0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000667 RID: 1639
		private int m_ApiVersion;

		// Token: 0x04000668 RID: 1640
		private IntPtr m_LocalUserId;

		// Token: 0x04000669 RID: 1641
		private IntPtr m_RoomName;

		// Token: 0x0400066A RID: 1642
		private float m_Volume;
	}
}
