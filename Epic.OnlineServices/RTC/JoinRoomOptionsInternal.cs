using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000181 RID: 385
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinRoomOptionsInternal : ISettable<JoinRoomOptions>, IDisposable
	{
		// Token: 0x1700029B RID: 667
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00010873 File Offset: 0x0000EA73
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700029C RID: 668
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00010883 File Offset: 0x0000EA83
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x1700029D RID: 669
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00010893 File Offset: 0x0000EA93
		public Utf8String ClientBaseUrl
		{
			set
			{
				Helper.Set(value, ref this.m_ClientBaseUrl);
			}
		}

		// Token: 0x1700029E RID: 670
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000108A3 File Offset: 0x0000EAA3
		public Utf8String ParticipantToken
		{
			set
			{
				Helper.Set(value, ref this.m_ParticipantToken);
			}
		}

		// Token: 0x1700029F RID: 671
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000108B3 File Offset: 0x0000EAB3
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x000108C3 File Offset: 0x0000EAC3
		public JoinRoomFlags Flags
		{
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000108CD File Offset: 0x0000EACD
		public bool ManualAudioInputEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_ManualAudioInputEnabled);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x000108DD File Offset: 0x0000EADD
		public bool ManualAudioOutputEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_ManualAudioOutputEnabled);
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000108F0 File Offset: 0x0000EAF0
		public void Set(ref JoinRoomOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ClientBaseUrl = other.ClientBaseUrl;
			this.ParticipantToken = other.ParticipantToken;
			this.ParticipantId = other.ParticipantId;
			this.Flags = other.Flags;
			this.ManualAudioInputEnabled = other.ManualAudioInputEnabled;
			this.ManualAudioOutputEnabled = other.ManualAudioOutputEnabled;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00010970 File Offset: 0x0000EB70
		public void Set(ref JoinRoomOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ClientBaseUrl = other.Value.ClientBaseUrl;
				this.ParticipantToken = other.Value.ParticipantToken;
				this.ParticipantId = other.Value.ParticipantId;
				this.Flags = other.Value.Flags;
				this.ManualAudioInputEnabled = other.Value.ManualAudioInputEnabled;
				this.ManualAudioOutputEnabled = other.Value.ManualAudioOutputEnabled;
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ClientBaseUrl);
			Helper.Dispose(ref this.m_ParticipantToken);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x0400051B RID: 1307
		private int m_ApiVersion;

		// Token: 0x0400051C RID: 1308
		private IntPtr m_LocalUserId;

		// Token: 0x0400051D RID: 1309
		private IntPtr m_RoomName;

		// Token: 0x0400051E RID: 1310
		private IntPtr m_ClientBaseUrl;

		// Token: 0x0400051F RID: 1311
		private IntPtr m_ParticipantToken;

		// Token: 0x04000520 RID: 1312
		private IntPtr m_ParticipantId;

		// Token: 0x04000521 RID: 1313
		private JoinRoomFlags m_Flags;

		// Token: 0x04000522 RID: 1314
		private int m_ManualAudioInputEnabled;

		// Token: 0x04000523 RID: 1315
		private int m_ManualAudioOutputEnabled;
	}
}
