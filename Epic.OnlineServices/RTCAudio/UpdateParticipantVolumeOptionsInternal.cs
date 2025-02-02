using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E7 RID: 487
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParticipantVolumeOptionsInternal : ISettable<UpdateParticipantVolumeOptions>, IDisposable
	{
		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0001427C File Offset: 0x0001247C
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700035B RID: 859
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0001428C File Offset: 0x0001248C
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x1700035C RID: 860
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0001429C File Offset: 0x0001249C
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x1700035D RID: 861
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x000142AC File Offset: 0x000124AC
		public float Volume
		{
			set
			{
				this.m_Volume = value;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000142B6 File Offset: 0x000124B6
		public void Set(ref UpdateParticipantVolumeOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Volume = other.Volume;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000142F4 File Offset: 0x000124F4
		public void Set(ref UpdateParticipantVolumeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Volume = other.Value.Volume;
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00014369 File Offset: 0x00012569
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x0400061E RID: 1566
		private int m_ApiVersion;

		// Token: 0x0400061F RID: 1567
		private IntPtr m_LocalUserId;

		// Token: 0x04000620 RID: 1568
		private IntPtr m_RoomName;

		// Token: 0x04000621 RID: 1569
		private IntPtr m_ParticipantId;

		// Token: 0x04000622 RID: 1570
		private float m_Volume;
	}
}
