using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001EB RID: 491
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateReceivingOptionsInternal : ISettable<UpdateReceivingOptions>, IDisposable
	{
		// Token: 0x1700036F RID: 879
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x00014734 File Offset: 0x00012934
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000370 RID: 880
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00014744 File Offset: 0x00012944
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x17000371 RID: 881
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00014754 File Offset: 0x00012954
		public ProductUserId ParticipantId
		{
			set
			{
				Helper.Set(value, ref this.m_ParticipantId);
			}
		}

		// Token: 0x17000372 RID: 882
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00014764 File Offset: 0x00012964
		public bool AudioEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_AudioEnabled);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00014774 File Offset: 0x00012974
		public void Set(ref UpdateReceivingOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.AudioEnabled = other.AudioEnabled;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000147B4 File Offset: 0x000129B4
		public void Set(ref UpdateReceivingOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.AudioEnabled = other.Value.AudioEnabled;
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00014829 File Offset: 0x00012A29
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_ParticipantId);
		}

		// Token: 0x04000633 RID: 1587
		private int m_ApiVersion;

		// Token: 0x04000634 RID: 1588
		private IntPtr m_LocalUserId;

		// Token: 0x04000635 RID: 1589
		private IntPtr m_RoomName;

		// Token: 0x04000636 RID: 1590
		private IntPtr m_ParticipantId;

		// Token: 0x04000637 RID: 1591
		private int m_AudioEnabled;
	}
}
