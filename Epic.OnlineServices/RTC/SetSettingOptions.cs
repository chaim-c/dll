using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000198 RID: 408
	public struct SetSettingOptions
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x000117CC File Offset: 0x0000F9CC
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public Utf8String SettingName { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000117DD File Offset: 0x0000F9DD
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x000117E5 File Offset: 0x0000F9E5
		public Utf8String SettingValue { get; set; }
	}
}
