using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000197 RID: 407
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetRoomSettingOptionsInternal : ISettable<SetRoomSettingOptions>, IDisposable
	{
		// Token: 0x170002C7 RID: 711
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x000116A3 File Offset: 0x0000F8A3
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x000116B3 File Offset: 0x0000F8B3
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x000116C3 File Offset: 0x0000F8C3
		public Utf8String SettingName
		{
			set
			{
				Helper.Set(value, ref this.m_SettingName);
			}
		}

		// Token: 0x170002CA RID: 714
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000116D3 File Offset: 0x0000F8D3
		public Utf8String SettingValue
		{
			set
			{
				Helper.Set(value, ref this.m_SettingValue);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000116E3 File Offset: 0x0000F8E3
		public void Set(ref SetRoomSettingOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.SettingName = other.SettingName;
			this.SettingValue = other.SettingValue;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00011724 File Offset: 0x0000F924
		public void Set(ref SetRoomSettingOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.SettingName = other.Value.SettingName;
				this.SettingValue = other.Value.SettingValue;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00011799 File Offset: 0x0000F999
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_SettingName);
			Helper.Dispose(ref this.m_SettingValue);
		}

		// Token: 0x04000556 RID: 1366
		private int m_ApiVersion;

		// Token: 0x04000557 RID: 1367
		private IntPtr m_LocalUserId;

		// Token: 0x04000558 RID: 1368
		private IntPtr m_RoomName;

		// Token: 0x04000559 RID: 1369
		private IntPtr m_SettingName;

		// Token: 0x0400055A RID: 1370
		private IntPtr m_SettingValue;
	}
}
