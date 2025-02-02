using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000185 RID: 389
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveRoomOptionsInternal : ISettable<LeaveRoomOptions>, IDisposable
	{
		// Token: 0x170002AE RID: 686
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x00010CE9 File Offset: 0x0000EEE9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002AF RID: 687
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x00010CF9 File Offset: 0x0000EEF9
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00010D09 File Offset: 0x0000EF09
		public void Set(ref LeaveRoomOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00010D30 File Offset: 0x0000EF30
		public void Set(ref LeaveRoomOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00010D7B File Offset: 0x0000EF7B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x0400052E RID: 1326
		private int m_ApiVersion;

		// Token: 0x0400052F RID: 1327
		private IntPtr m_LocalUserId;

		// Token: 0x04000530 RID: 1328
		private IntPtr m_RoomName;
	}
}
