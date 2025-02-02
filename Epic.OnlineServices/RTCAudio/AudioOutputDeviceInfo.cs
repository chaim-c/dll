using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B2 RID: 434
	public struct AudioOutputDeviceInfo
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00012907 File Offset: 0x00010B07
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0001290F File Offset: 0x00010B0F
		public bool DefaultDevice { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x00012918 File Offset: 0x00010B18
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00012920 File Offset: 0x00010B20
		public Utf8String DeviceId { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00012929 File Offset: 0x00010B29
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00012931 File Offset: 0x00010B31
		public Utf8String DeviceName { get; set; }

		// Token: 0x06000C6D RID: 3181 RVA: 0x0001293A File Offset: 0x00010B3A
		internal void Set(ref AudioOutputDeviceInfoInternal other)
		{
			this.DefaultDevice = other.DefaultDevice;
			this.DeviceId = other.DeviceId;
			this.DeviceName = other.DeviceName;
		}
	}
}
