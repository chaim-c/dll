using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001EF RID: 495
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingVolumeOptionsInternal : ISettable<UpdateReceivingVolumeOptions>, IDisposable
	{
		// Token: 0x17000381 RID: 897
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00014B53 File Offset: 0x00012D53
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000382 RID: 898
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00014B63 File Offset: 0x00012D63
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x17000383 RID: 899
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00014B73 File Offset: 0x00012D73
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00014B7D File Offset: 0x00012D7D
		public void Set(ref UpdateReceivingVolumeOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public void Set(ref UpdateReceivingVolumeOptions? other)
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

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00014C10 File Offset: 0x00012E10
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000645 RID: 1605
		private int m_ApiVersion;

		// Token: 0x04000646 RID: 1606
		private IntPtr m_LocalUserId;

		// Token: 0x04000647 RID: 1607
		private IntPtr m_RoomName;

		// Token: 0x04000648 RID: 1608
		private float m_Volume;
	}
}
