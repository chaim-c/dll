using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A3 RID: 419
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioOutputStateOptionsInternal : ISettable<AddNotifyAudioOutputStateOptions>, IDisposable
	{
		// Token: 0x170002DF RID: 735
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00011B98 File Offset: 0x0000FD98
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		public void Set(ref AddNotifyAudioOutputStateOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00011BDC File Offset: 0x0000FDDC
		public void Set(ref AddNotifyAudioOutputStateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00011C27 File Offset: 0x0000FE27
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000574 RID: 1396
		private int m_ApiVersion;

		// Token: 0x04000575 RID: 1397
		private IntPtr m_LocalUserId;

		// Token: 0x04000576 RID: 1398
		private IntPtr m_RoomName;
	}
}
