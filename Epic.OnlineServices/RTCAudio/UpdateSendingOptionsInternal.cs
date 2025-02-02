using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F3 RID: 499
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSendingOptionsInternal : ISettable<UpdateSendingOptions>, IDisposable
	{
		// Token: 0x17000392 RID: 914
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x00014F2B File Offset: 0x0001312B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000393 RID: 915
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x00014F3B File Offset: 0x0001313B
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x17000394 RID: 916
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x00014F4B File Offset: 0x0001314B
		public RTCAudioStatus AudioStatus
		{
			set
			{
				this.m_AudioStatus = value;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00014F55 File Offset: 0x00013155
		public void Set(ref UpdateSendingOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.AudioStatus = other.AudioStatus;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00014F88 File Offset: 0x00013188
		public void Set(ref UpdateSendingOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.AudioStatus = other.Value.AudioStatus;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00014FE8 File Offset: 0x000131E8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000656 RID: 1622
		private int m_ApiVersion;

		// Token: 0x04000657 RID: 1623
		private IntPtr m_LocalUserId;

		// Token: 0x04000658 RID: 1624
		private IntPtr m_RoomName;

		// Token: 0x04000659 RID: 1625
		private RTCAudioStatus m_AudioStatus;
	}
}
