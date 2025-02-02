using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000606 RID: 1542
	public struct RegisterEventOptions
	{
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0003B18F File Offset: 0x0003938F
		// (set) Token: 0x060027A2 RID: 10146 RVA: 0x0003B197 File Offset: 0x00039397
		public uint EventId { get; set; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x0003B1A0 File Offset: 0x000393A0
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x0003B1A8 File Offset: 0x000393A8
		public Utf8String EventName { get; set; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x0003B1B1 File Offset: 0x000393B1
		// (set) Token: 0x060027A6 RID: 10150 RVA: 0x0003B1B9 File Offset: 0x000393B9
		public AntiCheatCommonEventType EventType { get; set; }

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0003B1C2 File Offset: 0x000393C2
		// (set) Token: 0x060027A8 RID: 10152 RVA: 0x0003B1CA File Offset: 0x000393CA
		public RegisterEventParamDef[] ParamDefs { get; set; }
	}
}
