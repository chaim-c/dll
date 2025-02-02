using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000196 RID: 406
	public struct SetRoomSettingOptions
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0001165F File Offset: 0x0000F85F
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00011667 File Offset: 0x0000F867
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00011670 File Offset: 0x0000F870
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00011678 File Offset: 0x0000F878
		public Utf8String RoomName { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00011681 File Offset: 0x0000F881
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00011689 File Offset: 0x0000F889
		public Utf8String SettingName { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00011692 File Offset: 0x0000F892
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0001169A File Offset: 0x0000F89A
		public Utf8String SettingValue { get; set; }
	}
}
