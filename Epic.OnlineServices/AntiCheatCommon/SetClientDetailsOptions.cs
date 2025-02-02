using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200060A RID: 1546
	public struct SetClientDetailsOptions
	{
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x0003B3F5 File Offset: 0x000395F5
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x0003B3FD File Offset: 0x000395FD
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x0003B406 File Offset: 0x00039606
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x0003B40E File Offset: 0x0003960E
		public AntiCheatCommonClientFlags ClientFlags { get; set; }

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x0003B417 File Offset: 0x00039617
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x0003B41F File Offset: 0x0003961F
		public AntiCheatCommonClientInput ClientInputMethod { get; set; }
	}
}
