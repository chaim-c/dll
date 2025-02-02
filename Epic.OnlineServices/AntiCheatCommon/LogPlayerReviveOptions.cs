using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F0 RID: 1520
	public struct LogPlayerReviveOptions
	{
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00039BC5 File Offset: 0x00037DC5
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00039BCD File Offset: 0x00037DCD
		public IntPtr RevivedPlayerHandle { get; set; }

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x00039BD6 File Offset: 0x00037DD6
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x00039BDE File Offset: 0x00037DDE
		public IntPtr ReviverPlayerHandle { get; set; }
	}
}
