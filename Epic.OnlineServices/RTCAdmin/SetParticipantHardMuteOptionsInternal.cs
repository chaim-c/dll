using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020E RID: 526
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetParticipantHardMuteOptionsInternal : ISettable<SetParticipantHardMuteOptions>, IDisposable
	{
		// Token: 0x170003D4 RID: 980
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00015FF0 File Offset: 0x000141F0
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00016000 File Offset: 0x00014200
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00016010 File Offset: 0x00014210
		public bool Mute
		{
			set
			{
				Helper.Set(value, ref this.m_Mute);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00016020 File Offset: 0x00014220
		public void Set(ref SetParticipantHardMuteOptions other)
		{
			this.m_ApiVersion = 1;
			this.RoomName = other.RoomName;
			this.TargetUserId = other.TargetUserId;
			this.Mute = other.Mute;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00016054 File Offset: 0x00014254
		public void Set(ref SetParticipantHardMuteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.RoomName = other.Value.RoomName;
				this.TargetUserId = other.Value.TargetUserId;
				this.Mute = other.Value.Mute;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000160B4 File Offset: 0x000142B4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040006A1 RID: 1697
		private int m_ApiVersion;

		// Token: 0x040006A2 RID: 1698
		private IntPtr m_RoomName;

		// Token: 0x040006A3 RID: 1699
		private IntPtr m_TargetUserId;

		// Token: 0x040006A4 RID: 1700
		private int m_Mute;
	}
}
