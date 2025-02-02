using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AE RID: 430
	public struct AudioInputDeviceInfo
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000124FC File Offset: 0x000106FC
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x00012504 File Offset: 0x00010704
		public bool DefaultDevice { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001250D File Offset: 0x0001070D
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x00012515 File Offset: 0x00010715
		public Utf8String DeviceId { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0001251E File Offset: 0x0001071E
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x00012526 File Offset: 0x00010726
		public Utf8String DeviceName { get; set; }

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001252F File Offset: 0x0001072F
		internal void Set(ref AudioInputDeviceInfoInternal other)
		{
			this.DefaultDevice = other.DefaultDevice;
			this.DeviceId = other.DeviceId;
			this.DeviceName = other.DeviceName;
		}
	}
}
